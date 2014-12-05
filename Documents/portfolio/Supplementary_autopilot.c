/******************************
Version 1.4  

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
*******************************/

#include <Servo.h> 
#include <NewPing.h>
#include <PID_v1.h>
#include <Adafruit_LSM303_U.h>
#include <Adafruit_BMP085_U.h>
#include <Adafruit_L3GD20_U.h>
#include <Adafruit_Sensor.h>
#include <Wire.h>


/*  INPUT RANGE */
#define ZERO_THROTTLE  1000
#define FULL_THROTTLE  2300

#define FULL_STICK_LEFT_FORWARD  900
#define FULL_STICK_RIGHT_BACK    2100
#define ZERO_STICK               1500

/* Pin assignments. */
#define AILERONS_PIN  2
#define ELEVATOR_PIN  3
#define THROTTLE_PIN  4
#define RUDDER_PIN    5
#define AUX           6


/* Sonar Setup */
#define GROUND_PING_PIN           12 
#define GROUND_ECHO_PIN           11
#define GROUND_MAX_SONAR_DISTANCE 200
#define GROUND_SONAR_ITERATION    5

/*  PID Setup */
#define thr_out_range 1.25
#define Kp_add      0
#define Ki_add      1
#define Kd_add      2
#define auxset_add  3
#define pidMode_add 4

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
double                  altitude_hold, ground_range_value, throttle_position;
byte                    RangeTime_1cm;
int                     groundRangeTime;
Servo                   throttle, rudder, aileron, elevator, aux;
byte                    armed;
NewPing                 ground_range(GROUND_PING_PIN, GROUND_ECHO_PIN, GROUND_MAX_SONAR_DISTANCE);

/* Global Sensor Variables */
Adafruit_LSM303_Accel_Unified acc = Adafruit_LSM303_Accel_Unified(30301);
Adafruit_LSM303_Mag_Unified   mag   = Adafruit_LSM303_Mag_Unified(30302);
Adafruit_BMP085_Unified       bmp   = Adafruit_BMP085_Unified(18001);
Adafruit_L3GD20_Unified       gyro  = Adafruit_L3GD20_Unified(20);
sensors_event_t event;

/* Global PID Variables */
PID altPID(&ground_range_value, &throttle_position, &altitude_hold, 2, 5, 1, DIRECT);

/* Pin secttion. */
void setup_pins(){
  aileron.attach(AILERONS_PIN);
  elevator.attach(ELEVATOR_PIN);
  throttle.attach(THROTTLE_PIN);
  rudder.attach(RUDDER_PIN);
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
  throttle.writeMicroseconds(throttle_position);
  delay (1500);
  //displaySensorDetails();
}


void loop(){  
    
  ground_range_value = ground_range.ping_median(GROUND_SONAR_ITERATION);
  ground_range_value = (int)ground_range_value / RangeTime_1cm;
  
  pid_loop();
  
  acc.getEvent(&event);
  Serial.print(" Acc Z: ");
  Serial.print(event.acceleration.z);

  gyro.getEvent(&event);
  Serial.print(" gyro Z: ");
  Serial.print(event.gyro.z);

  Serial.print(" Ping: ");
  Serial.print(ground_range_value);
  Serial.println(" cm");
  
   
  
  //armed = arm();
  
  //delay (5000);
  
  //armed = disarm();

  //delay(10);  
}
