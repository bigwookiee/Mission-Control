/******************************
Version 1.5  
3/24/2015

Supplemental quad copter Autopilot.

 Contains a prototype controller algorithm
 to maintain an altitude hold. In this 
 version  only an ultrasonic rangefinder 
 is used to measure altitude. 

Important numbers for the kk2.1

center satick :1500 micro seconds
Full Right/Back: 2100
Full Left/Forward: 900

100% Throttle: 2300
0% Throttle: 1000

X-bee is on com 2 (serial 02).
GPS is on com 2 (serial 03).

*******************************/

#include <Servo.h> 
#include <NewPing.h>
#include <PID_v1.h>
#include <Adafruit_LSM303_U.h>
#include <Adafruit_BMP085_U.h>
#include <Adafruit_L3GD20_U.h>
#include <Adafruit_Sensor.h>
#include <Adafruit_NeoPixel.h>
#include <Wire.h>
#include <avr/power.h>
#include <PinChangeInt.h>



/* GPS PINS */
#define FIX   45  


/*  INPUT RANGE */
#define ZERO_THROTTLE  1000
#define FULL_THROTTLE  2300

#define FULL_STICK_LEFT_FORWARD  900
#define FULL_STICK_RIGHT_BACK    2100
#define ZERO_STICK               1500

/* PWM pin assignments. */
#define AILERONS_PIN  5
#define ELEVATOR_PIN  6
#define THROTTLE_PIN  7
#define RUDDER_PIN    8
#define AUX           9

/* Hand held RX inputs assignments.  */ 
#define RX_AILERONS   A15
#define RX_ELEVATOR   A14
#define RX_THROTTLE   A13
#define RX_RUDDER     A12
#define RX_AUX        A11

/* Bitflag used in interups to indecate a 
new signal on a channel.  

!!!Note this is not a pin number!!!

This is a power of 2. Next up would be 32, 
64, and 128.  If armed is moved hear use 128.*/

#define ALI_FLAG 1
#define ELE_FLAG 2
#define THR_FLAG 4
#define RUD_FLAG 8 
#define AUX_FLAG 16

/* Sonar Setup */
#define GROUND_PING_PIN           27 
#define GROUND_ECHO_PIN           26
#define GROUND_MAX_SONAR_DISTANCE 200
#define GROUND_SONAR_ITERATION    5

/*  PID Setup */
#define thr_out_range 1.25
#define Kp_add       0
#define Ki_add       1
#define Kd_add       2
#define auxset_add   3
#define pidMode_add  4

/*  LED Setup  */
#define LED_PIN      23
#define NUMPIXELS    16
#define HALFPIXELS   8       // Devide the pixels in have to allow one side of deone to be a defrent color.
#define L_SIDE       50,0,0  // For max intesity of red set 255,0,0
#define R_SIDE       0,50,0  // For max green set to 0,255,0 

void zero_stick();
void zero_all_inputs();
void set_thr(int);
void set_ali(int);
void set_elv(int);
void set_rud(int);
void setup_pins();
byte arm();
byte disarm();


/* Global Variables */
volatile uint8_t        bit_update_flags_shared;  //Used for interups
volatile uint16_t       ali_in_shared, ele_in_shared, thr_in_shared, rud_in_shared, aux_in_shared; //ISR shared inputs from RX
         uint32_t       ali_start, ele_start, thr_start, rud_start, aux_start; //Used to recored rising edge for ISR

double                  altitude_hold, ground_range_value, throttle_position;
byte                    RangeTime_1cm;
int                     groundRangeTime;
Servo                   throttle, rudder, aileron, elevator, aux;
byte                    armed;  //May move to bitUpdateFlagShared;
NewPing                 ground_range(GROUND_PING_PIN, GROUND_ECHO_PIN, GROUND_MAX_SONAR_DISTANCE);

char in_isr = 'F';
/* Global Sensor Variables */
Adafruit_LSM303_Accel_Unified acc = Adafruit_LSM303_Accel_Unified(30301);
Adafruit_LSM303_Mag_Unified   mag   = Adafruit_LSM303_Mag_Unified(30302);
Adafruit_BMP085_Unified       bmp   = Adafruit_BMP085_Unified(18001);
Adafruit_L3GD20_Unified       gyro  = Adafruit_L3GD20_Unified(20);
sensors_event_t event;

/* Global PID Variables */
PID altPID(&ground_range_value, &throttle_position, &altitude_hold, 2, 5, 1, DIRECT);

Adafruit_NeoPixel pixels = Adafruit_NeoPixel(NUMPIXELS, LED_PIN, NEO_GRB + NEO_KHZ800);

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


// ISR for Alierons
void calcAlierons()
{
  if(digitalRead(RX_AILERONS) == HIGH)
    ali_start = micros();
  else
  {
    ali_in_shared = (uint16_t) (micros() - ali_start);
    bit_update_flags_shared |= ALI_FLAG;
  }
  in_isr = 'T';
}

void calcElevator()
{
  if(digitalRead(RX_ELEVATOR) == HIGH)
    ele_start = micros();
  else
  {
    ele_in_shared = (uint16_t) (micros() - ele_start);
    bit_update_flags_shared |= ELE_FLAG;
  }
  in_isr = 'T';
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
  in_isr = 'T';
}

void calcRudder()
{
  if(digitalRead(RX_RUDDER) == HIGH)
    rud_start = micros();
  else
  {
    rud_in_shared = (uint16_t) (micros() - rud_start);
    bit_update_flags_shared |= RUD_FLAG;
  }
  in_isr = 'T';
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
  in_isr = 'T';
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

/* Relay remote throught arduino 
/*void remote_relay()
{
  volatile   unsigned int ch[8];
  
  ch[0] = pulseIn(RX_AILERONS, HIGH, 20000);
  ch[1] = pulseIn(RX_ELEVATOR, HIGH, 20000);
  ch[2] = pulseIn(RX_THROTTLE, HIGH, 20000);
  ch[3] = pulseIn(RX_RUDDER  , HIGH, 20000);
  ch[4] = pulseIn(RX_AUX     , HIGH, 20000);
  ch[5] = pulseIn(RX_CH6     , HIGH, 20000);
  ch[6] = pulseIn(RX_CH7     , HIGH, 20000);
  ch[7] = pulseIn(RX_CH8     , HIGH, 20000);
  
  //Signal Massaging
  for (int i=0; i<8; i++)      //Signal Conditioning loop
    {
      //ch[i]=constrain(ch[i], RXLo, RXHi);  //Trim bottom and upper end
      //if (ch[i] <= RXDeadHi && ch[i] >= RXDeadLo){     //Create Dead-Band
        //ch[i] = RXMid;}
    
      Serial.print("ch");
      Serial.print(i);
      Serial.print(":");
      Serial.print(ch[i]);
      Serial.print("|");
    }
    Serial.println();
  
    //aileron.writeMicroseconds(ch[0]);
    //elevator.writeMicroseconds(ch[1]);
    //throttle.writeMicroseconds(ch[2]);
    //rudder.writeMicroseconds(ch[3]);
    //aux.writeMicroseconds(ch[4]);
  
  
  
  rudder.writeMicroseconds(dur);
  elevator.writeMicroseconds(dur); 
  throttle.writeMicroseconds(dur);
  
}*/

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
byte arm(){
  set_thr(0);
  set_rud(-100);
  delay(1000);
  set_rud(0);
  
  return 1;
  
}

/* Disarms the motors */
byte  disarm(){
  set_thr(0);
  set_rud(100);
  delay(1000);
  set_rud(0);
  
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


void pid_loop(){
 int thr_min, thr_max;  // 
 
 //thr_min = throttle_position - thr_out_range;
 //thr_max = throttle_position + thr_out_range;
 //altPID.SetOutputLimits(thr_min,thr_max);
 altPID.Compute();
 Serial.print((int)throttle_position);
 throttle.writeMicroseconds((int)throttle_position);
}

void setup() {
  Serial.begin(115200);  
  Serial2.begin(9600);
  Serial3.begin(9600);
    
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
  altPID.SetOutputLimits(1000, 2300);
  throttle_position = 1500;
  altitude_hold = 20; 
  RangeTime_1cm = time_1cm();
  
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
  
  delay (1500);
  //displaySensorDetails();
}

void loop(){  
    
  //ground_range_value = ground_range.ping_median(GROUND_SONAR_ITERATION);
  //ground_range_value = (int)ground_range_value / RangeTime_1cm;
  
  //pid_loop();
  
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

  //delay(10);
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
      
  if(bit_update_flags & THR_FLAG)
    if(throttle.readMicroseconds() != thr_in)
      throttle.writeMicroseconds(thr_in);
      
  if(bit_update_flags & RUD_FLAG)
    if(rudder.readMicroseconds() != rud_in)
      rudder.writeMicroseconds(rud_in);
  
  if(bit_update_flags & AUX_FLAG)
    if(aux.readMicroseconds() != aux_in)
      aux.writeMicroseconds(aux_in);    

  bit_update_flags = 0;

  Serial.print("ali:");
  Serial.print(ali_in);
  Serial.print("|");
  
  Serial.print("ele:");
  Serial.print(ele_in);
  Serial.print("|");
  
  Serial.print("thr:");
  Serial.print(thr_in);
  Serial.print("|");

  Serial.print("rud:");
  Serial.print(rud_in);
  Serial.print("|");
  
  Serial.print("aux:");
  Serial.print(aux_in);
  Serial.print("|");
  
  Serial.print("ISR:");
  Serial.println(in_isr);


}
