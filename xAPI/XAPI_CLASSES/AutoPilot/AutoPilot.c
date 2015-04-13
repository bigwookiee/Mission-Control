#ifndef AutoPilot_C
#define AutoPilot_C
#include <AutoPilot.h>

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
  sensors_event_t acc_event;
  sensors_event_t mag_event;
  sensors_vec_t   orientation;
  //int thr_min, thr_max;  // 
 
  //thr_min = throttle_position - thr_out_range;
  //thr_max = throttle_position + thr_out_range;
  //altPID.SetOutputLimits(thr_min,thr_max);
  
  if (bit_autopilot_flags & ALTHOLD_FLAG)
  {
    ground_range_value = ground_range.ping_median(GROUND_SONAR_ITERATION);
    ground_range_value = (int)ground_range_value / RangeTime_1cm;
  
    Serial.print("Alt: ");
    
    altPID.Compute();
    Serial.print((int)throttle_position);
    throttle.writeMicroseconds((int)throttle_position);
  }
  
  if (bit_autopilot_flags & HEADING_FLAG)
  {
    acc.getEvent(&acc_event);
    /*if(dof.accelGetOrientation(&acc_event, &orientation))
    {
      Serial.print(F("Roll: "));
      Serial.print(orientation.roll);
      Serial.print(F("; "));
      Serial.print(F("Pitch: "));
      Serial.print(orientation.pitch);
      Serial.print(F("; "));
    }
    */
    
    mag.getEvent(&mag_event);
    if (dof.magGetOrientation(SENSOR_AXIS_Z, &mag_event, &orientation))
    {
      /* 'orientation' should have valid .heading data now */
      Serial.print("AutoPilot NO !!!!!! ");
      Serial.print(F("Heading: "));
      Serial.print(orientation.heading);
      Serial.print(F("; "));
    }
     
     current_heading = orientation.heading;
     
     headingPID.Compute();
     Serial.println((int)rudder_position);
     rudder.writeMicroseconds((int)rudder_position);
  }
 
}

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
      
  if(bit_update_flags & THR_FLAG)
    if(throttle.readMicroseconds() != thr_in)
      throttle.writeMicroseconds(thr_in);
      
  if((bit_update_flags & RUD_FLAG) && !(bit_autopilot_flags & HEADING_FLAG))
  {
    Serial.print("Manual ");
    if(rudder.readMicroseconds() != rud_in)
      rudder.writeMicroseconds(rud_in);
  }
  
  if(bit_update_flags & AUX_FLAG)
    if(aux_in > 1500)
    {
      
      //Serial.println(" IN IF");
      bit_autopilot_flags |= HEADING_FLAG;
    }  
    else
    { 
      bit_autopilot_flags &= ~(HEADING_FLAG); 
      //Serial.println("IN Else");
    }
      
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
  
  Serial.print("HED:");
  Serial.println(bit_autopilot_flags);
  
}



/*************  Setup  **************************/
void AutoPilot_setup() {
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
  altPID.SetOutputLimits(1000, 2000);
  throttle_position = 1500;  
  altitude_to_hold = 20; 
  RangeTime_1cm = time_1cm();
  
  
  
  headingPID.SetMode(AUTOMATIC);
  headingPID.SetOutputLimits(1000, 2000);
  rudder_position = 1500; 
  heading_to_hold = 90;
  
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


/************************ AutoPilot ****************************/

void AutoPilot(){  
    
  //ground_range_value = ground_range.ping_median(GROUND_SONAR_ITERATION);
  //ground_range_value = (int)ground_range_value / RangeTime_1cm;
  
 
 pid_loop();
 RX_pass(); 
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

#endif