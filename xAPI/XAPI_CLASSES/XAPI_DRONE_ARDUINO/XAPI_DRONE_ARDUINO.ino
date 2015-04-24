#include <Serial_service.h>
#include <LiquidCrystal.h>
#include <LCD_service.h>
#include <Xapi.h>
#include <Subscriptions.h>
#include <Universal.h>
#include <Util.h>
#include <Single_buff.h>
#include <Land_service.h>
#include <Takeoff_service.h>
#include <DoMove_service.h>
#include <Arm_service.h>
#include <Heartbeat_service.h>
#include <Servo.h> 
#include <NewPing.h>
#include <PID_v1.h>
#include <Adafruit_10DOF.h>
#include <Adafruit_LSM303_U.h>
#include <Adafruit_BMP085_U.h>
#include <Adafruit_L3GD20_U.h>
#include <Adafruit_Sensor.h>
#include <Adafruit_NeoPixel.h>
#include <Wire.h>
#include <avr/power.h>
#include <PinChangeInt.h>
#include <AutoPilot.h>
#include <AltHold_service.h>
#include <Heading_service.h>
#include <HeadingHold_service.h>
/*
struct drone_state
{
  double     current_head; // deg. The current heading.
  double     hold_head;    // deg. The heading to hold, this is independend of
                           //      course. 
  double     current_alt;  // cm Above sea level(ASL). 
  double     hold_alt;     // cm Altitude to hold above sea level(ASL). 
  double     ground_alt;   // cm The ground level when armed(AGL) ie. ASL when armed.    
  double     sonar_alt;    // cm The returned from range finder(AGL).
  double     current_lon;  // Longitude
  double     target_lon;   // Longitude I want to go to.
  double     current_lat;  // Latatude
  double     target_lat;   // Latatide I want to travil to.
  uint16_t   loiter_time;  // Time on station when drone arives.
};*/


/* Global Variables */
          uint8_t       bit_autopilot_flags;  // Used to keep track of auto pilot enabled subsytems.
volatile  uint8_t       bit_update_flags_shared;  //Used for interups
volatile uint16_t       ali_in_shared, ele_in_shared, thr_in_shared, rud_in_shared, aux_in_shared; //ISR shared inputs from RX
         uint32_t       ali_start, ele_start, thr_start, rud_start, aux_start; //Used to recored rising edge for ISR

waypoint                *head_waypoint;
drone_state             *P_state;
double                  c_alt,throttle_position,s_alt;  //throttle settings
                                                        //c_alt is current alt, s_alt is setpoint for PID
double                  c_head,rudder_position,s_head;  //rudder settings
                                                        //c_head is current heading, s_head is setpoint for PID
byte                    RangeTime_1cm;
int                     groundRangeTime;
Servo                   throttle, rudder, aileron, elevator, aux;
byte                    armed;  //May move to bitUpdateFlagShared;
NewPing                 ground_range(GROUND_PING_PIN, GROUND_ECHO_PIN, GROUND_MAX_SONAR_DISTANCE);

/* Global Sensor Variables */
Adafruit_LSM303_Accel_Unified acc = Adafruit_LSM303_Accel_Unified(30301);
Adafruit_LSM303_Mag_Unified   mag   = Adafruit_LSM303_Mag_Unified(30302);
Adafruit_BMP085_Unified       bmp   = Adafruit_BMP085_Unified(18001);
Adafruit_L3GD20_Unified       gyro  = Adafruit_L3GD20_Unified(20);
Adafruit_10DOF                dof   = Adafruit_10DOF();

/* Global PID Variables */
PID altPID(&P_state->sonar_alt, &throttle_position, &P_state->hold_alt, 1, .05, .25, DIRECT);
PID headingPID(&c_head, &rudder_position, &s_head, Kp, Ki, Kd, DIRECT);
//PID headingPID(&P_state->current_head, &rudder_position, &P_state->hold_head, 1, .05, .25, DIRECT);

/* Global NeoPixels */
Adafruit_NeoPixel pixels = Adafruit_NeoPixel(NUMPIXELS, LED_PIN, NEO_GRB + NEO_KHZ800);

//0013a200
//40a1446d

//***********************************************
//***********************************************

Xapi xapi = Xapi(Serial2);
//Serial_service serial_service = Serial_service(Serial1, xapi);
LCD_service lcd_service (xapi);
//************************************************
// The next services pertain to drone instructions
//************************************************
Takeoff_service takeoff_service(xapi, lcd_service);
Land_service land_service(xapi, lcd_service);
DoMove_service doMove_service(xapi, lcd_service);
Arm_service arm_service(xapi, lcd_service);
AltHold_service altHold_service(xapi, lcd_service);
Heading_service heading_service(xapi, lcd_service);
HeadingHold_service headingHold_service(xapi, lcd_service);

//Heartbeat_service heartbeat_service(xapi, lcd_service);
//Serial_service serial_service = Serial_service(Serial1, xapi, lcd_service);
//uint8_t msg1[] =   "I FEEL GREAT";
//uint8_t msg2[] =   "COMMODORE 64";
//uint8_t _clear[] = "                ";

//***********************************************
//***********************************************/
void setup()
{
 //Serial2.begin(MISC_PC_SPEED);
 //Serial1.begin(MISC_PC_SPEED);
 
 Serial.begin(115200); 

 AutoPilot_setup();  
}

//***********************************************
//***********************************************
void loop()
{
 //Serial.print("I live"); 
  
  //system_active();
  //process_buttons();
  AutoPilot();
  xapi.xapi_latch();
  lcd_service.lcd_service_latch();
  takeoff_service.takeoff_service_latch();
  land_service.land_service_latch();
  doMove_service.DoMove_service_latch();
  arm_service.arm_service_latch();
  altHold_service.altHold_service_latch();
  heading_service.heading_service_latch();
  headingHold_service.headingHold_service_latch();
  //heartbeat_service.heartbeat_service_latch();
  //serial_service.serial_service_latch();  
}



// ISR for Alierons
void calcAlierons()
{
  if(digitalRead(RX_AILERONS) == HIGH)
    ali_start = micros();
  else
  {
    ali_in_shared = (uint16_t) (micros() - ali_start);
    
    if ((ali_in_shared > LOW_CENTER_STICK) && (ali_in_shared < HIGH_CENTER_STICK))
      ali_in_shared = 1500;
    
    bit_update_flags_shared |= ALI_FLAG;
  }
  
}

void calcElevator()
{
  if(digitalRead(RX_ELEVATOR) == HIGH)
    ele_start = micros();
  else
  {
    ele_in_shared = (uint16_t) (micros() - ele_start);
    
      if ((ele_in_shared > LOW_CENTER_STICK) && (ele_in_shared < HIGH_CENTER_STICK))
      ele_in_shared = 1500;    

    bit_update_flags_shared |= ELE_FLAG;
  }
}

void calcThrottle()
{
  if(digitalRead(RX_THROTTLE) == HIGH)
    thr_start = micros();
  else
  {
    thr_in_shared = (uint16_t) (micros() - thr_start);
    bit_update_flags_shared |= THR_FLAG;
  }
}

void calcRudder()
{
  if(digitalRead(RX_RUDDER) == HIGH)
    rud_start = micros();
  else
  {
    rud_in_shared = (uint16_t) (micros() - rud_start);
    
    if ((rud_in_shared > LOW_CENTER_STICK) && (rud_in_shared < HIGH_CENTER_STICK))
      rud_in_shared = 1500;
    
    bit_update_flags_shared |= RUD_FLAG;
  }  
}

void calcAux()
{
  if(digitalRead(RX_AUX) == HIGH)
    aux_start = micros();
  else
  {
    aux_in_shared = (uint16_t) (micros() - aux_start);
    bit_update_flags_shared |= AUX_FLAG;
  }
}

/*  Zero out  the control x,y and rotational inputs. */
void zero_stick(){
  aileron.writeMicroseconds(ZERO_STICK);
  elevator.writeMicroseconds(ZERO_STICK);
  rudder.writeMicroseconds(ZERO_STICK);  
}

void zero_all_inputs(){
  zero_stick();
  set_thr(0);
}

/* Sets the throttle. Use a range of 0 to 100. */
void set_thr (int val){ 
  throttle.writeMicroseconds(map(val, 0, 100, ZERO_THROTTLE, FULL_THROTTLE));
  //Serial.println(map(val, 0, 100, ZERO_THROTTLE, FULL_THROTTLE));  
}

/*  Set the ailerons. Use a range of -100 to 100. */
void set_ail (int val) {
  if (val == 0)
    aileron.writeMicroseconds(ZERO_STICK);
  else
    aileron.writeMicroseconds(map(val, -100, 100, FULL_STICK_LEFT_FORWARD, FULL_STICK_RIGHT_BACK));
}

/*  Set the  elevator. Use a range of -100 to 100. */
void set_elv (int val) {
  if (val == 0)
    elevator.writeMicroseconds(ZERO_STICK);
  else
     elevator.writeMicroseconds(map(val, -100, 100, FULL_STICK_LEFT_FORWARD, FULL_STICK_RIGHT_BACK));
}

/*  Set the  rudder. Use a range of -100 to 100. */
void set_rud (int val) {
  if (val == 0)
    rudder.writeMicroseconds(ZERO_STICK);
  else
    rudder.writeMicroseconds(map(val, -100, 100, FULL_STICK_LEFT_FORWARD, FULL_STICK_RIGHT_BACK));
}

/* Arms the motors */
byte arm()
{
  P_state->current_alt = cmAlt();
  P_state->ground_alt  = cmAlt();
  throttle.writeMicroseconds(1000);
  aileron.writeMicroseconds(1100);
  rudder.writeMicroseconds(1100);
  delay(1000);
  aileron.writeMicroseconds(1500);
  rudder.writeMicroseconds(1500);
    
  return 1;
}

/* Disarms the motors */
byte  disarm()
{
  throttle.writeMicroseconds(1000);
  aileron.writeMicroseconds(1900);
  rudder.writeMicroseconds(1900);
  delay(1000);
  aileron.writeMicroseconds(1500);
  rudder.writeMicroseconds(1500);
  return(0);
}

byte time_1cm(){
  float temperature, time;
  bmp.getTemperature(&temperature);
  time = 2/((331.3 * sqrt(1 + temperature / 273.15))/10000);
  
//  Serial.print(temperature);
//  Serial.print(" C, time: ");
//  Serial.println(time,6);
  return time;
}

/*************************** PID LOOP **********************/

void pid_loop(){
  sensors_event_t acc_event;
  sensors_event_t mag_event;
  sensors_vec_t   orientation;

  //int thr_min, thr_max;  // 
 
  //thr_min = throttle_position - thr_out_range;
  //thr_max = throttle_position + thr_out_range;
  //altPID.SetOutputLimits(thr_min,thr_max);
 
  //Serial.print("In PID! ");
 
  if (bit_autopilot_flags & ALTHOLD_FLAG)
  { 
    P_state->sonar_alt = ground_range.ping_median(GROUND_SONAR_ITERATION);
    P_state->sonar_alt = (int)(P_state->sonar_alt / RangeTime_1cm) - CALIBRATION_ERROR;
    
    if (P_state->sonar_alt < 500)
      P_state->current_alt = P_state->ground_alt + P_state->sonar_alt;
    else
      P_state->current_alt = cmAlt();
    
    Serial.print("I am taking off! alt to hold at:");
    Serial.print(P_state->hold_alt);
    Serial.print(", current alt: ");
    Serial.print(P_state->current_alt);
    Serial.print(", Sonar: ");
    Serial.println(P_state->sonar_alt);
    
    altPID.Compute();
    Serial.print((int)throttle_position);
    throttle.writeMicroseconds((int)throttle_position);
  }

  if (bit_autopilot_flags & HEADING_FLAG)
  {
    //Serial.println("In hold heading! ");  
    acc.getEvent(&acc_event);
    //dof.accelGetOrientation(&acc_event, &orientation);
    if(dof.accelGetOrientation(&acc_event, &orientation))
    {
      Serial.print(F("Roll: "));
      Serial.print(orientation.roll);
      Serial.print(F("; "));
      Serial.print(F("Pitch: "));
      Serial.print(orientation.pitch);
      Serial.print(F("; "));
    }
        
    mag.getEvent(&mag_event);
    //dof.magGetOrientation(SENSOR_AXIS_Z, &mag_event, &orientation);
    if (dof.magGetOrientation(SENSOR_AXIS_Z, &mag_event, &orientation))
    {
      /* 'orientation' should have valid .heading data now */
      //Serial.print("AutoPilot ON !!!!!! ");
      Serial.print(F("Heading: "));
      Serial.print(orientation.heading);
      Serial.print(F("; "));
    }
     
     P_state->current_head = orientation.heading;
     c_head = P_state->current_head;  // The pid libary dose not like &P_state
     s_head = P_state->hold_head;     // for some reason.  Some thing to do with
                                      //  C++ vs C
     headingPID.Compute();
     Serial.print((int)rudder_position);
     rudder.writeMicroseconds((int)rudder_position);

     Serial.print("current Head: ");Serial.print(P_state->current_head); 
     Serial.print(", hold: "); Serial.print(P_state->hold_head);
     Serial.print(", alt_hold: "); Serial.println(P_state->hold_alt);
  }
}

/************************************** RX PASS ************************************/

void RX_pass(){

  // local variables to hold a local copies of the channel inputs.  
  static uint16_t ali_in;
  static uint16_t ele_in;
  static uint16_t thr_in;
  static uint16_t rud_in;
  static uint16_t aux_in; 
  static uint8_t bit_update_flags; // local copy of update flag

  if(bit_update_flags_shared)
  {
    noInterrupts();
    
    bit_update_flags = bit_update_flags_shared;
    
    if(bit_update_flags & ALI_FLAG)
      ali_in = ali_in_shared;
    
    if(bit_update_flags & ELE_FLAG)
      ele_in = ele_in_shared;
      
    if(bit_update_flags & THR_FLAG)
      thr_in = thr_in_shared;
      
    if(bit_update_flags & RUD_FLAG)
      rud_in = rud_in_shared;
      
    if(bit_update_flags & AUX_FLAG)
      aux_in = aux_in_shared;

    bit_update_flags_shared = 0; //clears up date flag so it can be set in the next time an interup accures.
    
    interrupts();  // Don't play with the shared copies anymore.  ISR owns them now.
  }
  
  if(bit_update_flags & ALI_FLAG)
    if(aileron.readMicroseconds() != ali_in)
      aileron.writeMicroseconds(ali_in);
  
  if(bit_update_flags & ELE_FLAG)
    if(elevator.readMicroseconds() != ele_in)
      elevator.writeMicroseconds(ele_in);
      
  if((bit_update_flags & THR_FLAG) && (!(bit_autopilot_flags & ALTHOLD_FLAG) 
      || !(bit_autopilot_flags & TOGGLE_AUTOPILOT))) 
    if(throttle.readMicroseconds() != thr_in)
    {
      throttle.writeMicroseconds(thr_in);
      //Serial.print("thr:");
      //Serial.println(thr_in);
    }
  
  //Serial.print(bit_autopilot_flags, BIN); Serial.println (" ");
  
  if((bit_update_flags & RUD_FLAG) && (!(bit_autopilot_flags & HEADING_FLAG) 
      || !(bit_autopilot_flags & TOGGLE_AUTOPILOT))) 
  {
    //Serial.println("Manual");
    if(rudder.readMicroseconds() != rud_in)
      rudder.writeMicroseconds(rud_in);
    }
  
  if(bit_update_flags & AUX_FLAG)
    if(aux_in > 1500)
    {
      bit_autopilot_flags |= TOGGLE_AUTOPILOT;
      //Serial.print("In if "); Serial.println(bit_autopilot_flags, BIN);
    }  
    else
    { 
      bit_autopilot_flags &= ~(TOGGLE_AUTOPILOT); 
      //Serial.print("In else "); Serial.println(bit_autopilot_flags, BIN);
    }
      
  bit_update_flags = 0;

  /*
  Serial.print("ali:");
  Serial.print(ali_in);
  Serial.print("|");
  
  Serial.print("ele:");
  Serial.print(ele_in);
  Serial.print("|");
  */
  //Serial.print("thr:");
  //Serial.println(thr_in);
  //Serial.print("|");
/*
  Serial.print("rud:");
  Serial.print(rud_in);
  Serial.print("|");
  
  Serial.print("aux:");
  Serial.print(aux_in);
  Serial.print("|");
  
  Serial.print("HED:");
  Serial.println(bit_autopilot_flags);
  */
}



/***********************  Setup  **************************************/

void AutoPilot_setup() {
  Serial.begin(115200);  
  Serial2.begin(9600);
  Serial3.begin(9600);
  Serial.println("Initialising drone");
      
  if(!acc.begin())
  {
    /* There was a problem detecting the ADXL345 ... check your connections */
    Serial.println(F("Ooops, no LSM303 detected ... Check your wiring!"));
    while(1);
  }
  if(!mag.begin())
  {
    /* There was a problem detecting the LSM303 ... check your connections */
    Serial.println("Ooops, no LSM303 detected ... Check your wiring!");
    while(1);
  }
  if(!bmp.begin())
  {
    /* There was a problem detecting the BMP085 ... check your connections */
    Serial.print("Ooops, no BMP085 detected ... Check your wiring or I2C ADDR!");
    while(1);
  }
  if(!gyro.begin())
  {
    /* There was a problem detecting the L3GD20 ... check your connections */
    Serial.print("Ooops, no L3GD20 detected ... Check your wiring or I2C ADDR!");
    while(1);
  }
  
  altPID.SetMode(AUTOMATIC);
  altPID.SetOutputLimits(1000, 2000);
  throttle_position = 1000;  
  RangeTime_1cm = time_1cm();
  
  headingPID.SetMode(AUTOMATIC);
  headingPID.SetOutputLimits(1350, 1650);
  rudder_position = 1500; 
  
  setup_pins(); 
  zero_all_inputs();
  //throttle.writeMicroseconds(throttle_position);
  
  pixels.begin();
  for(int i=0;i<HALFPIXELS;i++)
  {
    pixels.setPixelColor(i,pixels.Color(L_SIDE));
    pixels.setPixelColor(NUMPIXELS-1-i,pixels.Color(R_SIDE));
  }
  pixels.show();

  P_state = drone_state_new();
  
  Serial.print(P_state->current_alt);
  Serial.print("|");
  Serial.println(P_state->current_head);

  bit_autopilot_flags |= HEADING_FLAG;

  delay (1500);
}


/************************ AutoPilot ****************************/

void AutoPilot(){  
    
  //ground_range_value = ground_range.ping_median(GROUND_SONAR_ITERATION);
  //ground_range_value = (int)ground_range_value / RangeTime_1cm;
 if (bit_autopilot_flags & TOGGLE_AUTOPILOT)
   pid_loop();
   
 RX_pass();
 
 //Serial.println(bit_autopilot_flags, HEX);
 //delay(1000);
 
 //bit_autopilot_flags |= 128;
 
 
 
 if (bit_autopilot_flags & ARM_FLAG)
   if (!(bit_autopilot_flags & IS_ARMED_FLAG))
   {
     //Serial.println("I am armed ");
     arm();
     bit_autopilot_flags |= IS_ARMED_FLAG;
   }
 
 //Serial.println(bit_autopilot_flags, HEX);
 //delay(1000);
 
 //bit_autopilot_flags &= ~128;
 
 if (!(bit_autopilot_flags & ARM_FLAG))
   if (bit_autopilot_flags & IS_ARMED_FLAG)
   {
     disarm();
     bit_autopilot_flags &= ~IS_ARMED_FLAG;
     //Serial.println("I am disarmed");   
   }
 
 //Serial.println(bit_autopilot_flags, HEX);
 //delay(1000);
 
  //remote_relay();
  
  //acc.getEvent(&event);
  //Serial.print(" Acc Z: ");
  //Serial.print(event.acceleration.z);

  //gyro.getEvent(&event);
  //Serial.print(" gyro Z: ");
  //Serial.print(event.gyro.z);

  //Serial.print(" Ping: ");
  //Serial.print(ground_range_value);
  //Serial.println(" cm");

  //armed = arm();
  
  //delay (5000);
  
  //armed = disarm();
}

/* Pin secttion. */
  void setup_pins(){
  aileron.attach(AILERONS_PIN);
  elevator.attach(ELEVATOR_PIN);
  throttle.attach(THROTTLE_PIN);
  rudder.attach(RUDDER_PIN);
  
  attachPinChangeInterrupt(RX_AILERONS, calcAlierons, CHANGE);
  attachPinChangeInterrupt(RX_ELEVATOR, calcElevator, CHANGE);
  attachPinChangeInterrupt(RX_THROTTLE, calcThrottle, CHANGE);
  attachPinChangeInterrupt(RX_RUDDER  , calcRudder  , CHANGE);
  attachPinChangeInterrupt(RX_AUX     , calcAux     , CHANGE);
}


/*************************** drone_state ******************/
drone_state * drone_state_new()
{
  sensors_event_t acc_event;
  sensors_event_t mag_event;
  
  sensors_vec_t   orientation;
  drone_state     *p;
  
  acc.getEvent(&acc_event); 
  mag.getEvent(&mag_event);
  
  dof.magGetOrientation(SENSOR_AXIS_Z, &mag_event, &orientation);

  if((p = (drone_state *)malloc(sizeof *p)) != NULL)
  {
    p->current_head = orientation.heading;
    p->hold_head    = HOLD_HEADING;
    p->current_alt  = cmAlt();
    p->hold_alt     = HOLD_ALTITUDE;
    p->ground_alt   = p->current_alt;
    p->sonar_alt    = 0;
  }
  return p;
}

waypoint * waypoint_new(double heading, double alt, double lat, double lon)
{
  waypoint *p;
  
  if((p = (waypoint *)malloc(sizeof *p)) != NULL)
  {
    p->hold_head  = heading;
    p->hold_alt   = alt;
    p->target_lat = lat;
    p->target_lon = lon;
  }
  return p;
}

waypoint * waypoint_new(double heading, double alt, double lat, double lon, uint16_t time)
{
 waypoint *p;

 p = waypoint_new(heading, alt, lat, lon);
 p->loiter_time = time;
}

uint32_t cmAlt()
{
  sensors_event_t bmp_event;
  bmp.getEvent(&bmp_event);
  return bmp.pressureToAltitude(SENSORS_PRESSURE_SEALEVELHPA,bmp_event.pressure)*100;
}

/*
void displaySensorDetails(void)
{
  sensor_t sensor;
  
  accel.getSensor(&sensor);
  Serial.println(F("----------- ACCELEROMETER ----------"));
  Serial.print  (F("Sensor:       ")); Serial.println(sensor.name);
  Serial.print  (F("Driver Ver:   ")); Serial.println(sensor.version);
  Serial.print  (F("Unique ID:    ")); Serial.println(sensor.sensor_id);
  Serial.print  (F("Max Value:    ")); Serial.print(sensor.max_value); Serial.println(F(" m/s^2"));
  Serial.print  (F("Min Value:    ")); Serial.print(sensor.min_value); Serial.println(F(" m/s^2"));
  Serial.print  (F("Resolution:   ")); Serial.print(sensor.resolution); Serial.println(F(" m/s^2"));
  Serial.println(F("------------------------------------"));
  Serial.println(F(""));

  gyro.getSensor(&sensor);
  Serial.println(F("------------- GYROSCOPE -----------"));
  Serial.print  (F("Sensor:       ")); Serial.println(sensor.name);
  Serial.print  (F("Driver Ver:   ")); Serial.println(sensor.version);
  Serial.print  (F("Unique ID:    ")); Serial.println(sensor.sensor_id);
  Serial.print  (F("Max Value:    ")); Serial.print(sensor.max_value); Serial.println(F(" rad/s"));
  Serial.print  (F("Min Value:    ")); Serial.print(sensor.min_value); Serial.println(F(" rad/s"));
  Serial.print  (F("Resolution:   ")); Serial.print(sensor.resolution); Serial.println(F(" rad/s"));
  Serial.println(F("------------------------------------"));
  Serial.println(F(""));
  
  mag.getSensor(&sensor);
  Serial.println(F("----------- MAGNETOMETER -----------"));
  Serial.print  (F("Sensor:       ")); Serial.println(sensor.name);
  Serial.print  (F("Driver Ver:   ")); Serial.println(sensor.version);
  Serial.print  (F("Unique ID:    ")); Serial.println(sensor.sensor_id);
  Serial.print  (F("Max Value:    ")); Serial.print(sensor.max_value); Serial.println(F(" uT"));
  Serial.print  (F("Min Value:    ")); Serial.print(sensor.min_value); Serial.println(F(" uT"));
  Serial.print  (F("Resolution:   ")); Serial.print(sensor.resolution); Serial.println(F(" uT"));  
  Serial.println(F("------------------------------------"));
  Serial.println(F(""));

  bmp.getSensor(&sensor);
  Serial.println(F("-------- PRESSURE/ALTITUDE ---------"));
  Serial.print  (F("Sensor:       ")); Serial.println(sensor.name);
  Serial.print  (F("Driver Ver:   ")); Serial.println(sensor.version);
  Serial.print  (F("Unique ID:    ")); Serial.println(sensor.sensor_id);
  Serial.print  (F("Max Value:    ")); Serial.print(sensor.max_value); Serial.println(F(" hPa"));
  Serial.print  (F("Min Value:    ")); Serial.print(sensor.min_value); Serial.println(F(" hPa"));
  Serial.print  (F("Resolution:   ")); Serial.print(sensor.resolution); Serial.println(F(" hPa"));  
  Serial.println(F("------------------------------------"));
  Serial.println(F(""));
  
  delay(1500);
}
*/
