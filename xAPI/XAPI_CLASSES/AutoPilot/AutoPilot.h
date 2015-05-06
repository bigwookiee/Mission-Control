//#ifndef AutoPilot_H
//#define AutoPilot_H

/******************************
Supplemental quad copter Autopilot.
Arthur David Klingenberg


 Contains a prototype controller algorithm
 to implement an autopilot.

4/25/2015
Version 1.8  
-- Final version for expo

	added SD card loging
	added diagnostic LEDs
	reworked HOLD PID.
	added waypoints
	added drone stats.
	added rx heatbeat to shut down
	    drone if the signal is lost.
	added throttle kill if master autopilot
		is turned off to force drone to land.


Important numbers for the kk2.1

center satick :1500 micro seconds
Full Right/Back: 2100
Full Left/Forward: 900

100% Throttle: 2300
0% Throttle: 1000

X-bee is on com 2 (serial 02).
GPS is on com 2 (serial 03).

version history
4/15/2015 1.7
--First attempted at berring hold.


4/3/2015 1.6
--Compleatly scraped and replaced RX pass throught code.

3/24/2015 1.5
--added passthrought controll for rx.

12/10/2014 1.4  
 Contains a prototype controller algorithm
 to maintain an altitude hold. In this 
 version  only an ultrasonic rangefinder 
 is used to measure altitude. 

1.0 throught 1.3 fall 2015
sensore expermentations.

*******************************/
/* Debug and log file settings. */
//Change to a one to enable debug LEDs
#define DEBUG	1
#define LOG_NAME 04262015001

/* GPS PINS */
#define FIX   45  

/*  INPUT RANGE */
#define ZERO_THROTTLE  1000
#define FULL_THROTTLE  2000

#define FULL_STICK_LEFT_FORWARD  1000
#define FULL_STICK_RIGHT_BACK    2000
#define ZERO_STICK               1500

/* Defualt test hold data. */
#define HOLD_HEADING  270
#define HOLD_ALTITUDE 50


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

/* RX Max miss pulse length */
// Used in rx_on() function.
#define SAFE_PULSE    320

/* RX center stick values */
#define HIGH_CENTER_STICK 	1515
#define LOW_CENTER_STICK 	1475

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

/* Bitflag used to indecate autopilot status.
This ius a power of 2. valied numbers are
1,2,4,8,16,32,64,128
0x01 0x02 0x04 0x08 0x10 0x20 0x40 0x80
0b00000001 0b00000010 ... 0b10000000
*/

#define HEADING_FLAG 	 1    //Set autopilot to hold a heading.
#define ALTHOLD_FLAG 	 2    //
#define LATHOLD_FLAG     4	  //
#define LONGHOLD_FLAG    8    //

#define ARM_FLAG     	 16    //
#define IS_ARMED_FLAG	 32    //
#define MASTER_ON_FLAG	 64	   //
#define MASTER_AUTOPILOT 128   //Set by aux switch on remote.

/* Sonar Setup */
#define GROUND_PING_PIN           27 
#define GROUND_ECHO_PIN           26
#define GROUND_MAX_SONAR_DISTANCE 200
#define GROUND_SONAR_ITERATION    5
#define CALIBRATION_ERROR         9

/*  PID Setup */
//#define thr_out_range 1.25
#define Kp           40
#define Ki           10
#define Kd	         1
#define auxset_add   3
#define pidMode_add  4
#define SAMPLE_TIME  100

/*  LED Setup  */
#define LED_UPDATE_DELAY    100
#define LED_PIN      		23
#define NUMPIXELS    		16
#define HALFPIXELS  		8           // Devide the pixels in have to allow one side of deone to be a defrent color.
#define L_SIDE  	     	50,0,0      // For max intesity of red set 255,0,0
#define R_SIDE	            0,50,0      // For max green set to 0,255,0 
#define M_AUTOPIOT_ON   	50,50,50    // White
#define AUTOPILOT_HOLD_ON	0,0,50      // Blue
struct drone_state
{
  int     current_head; // deg. The current heading.
  int     hold_head;   // deg. The heading to hold, this is independend of
                           //      course. 
  double     current_alt;  // cm Above sea level(ASL). 
  double     hold_alt;     // cm Altitude to hold above sea level(ASL). 
  double     ground_alt;   // cm The ground level when armed(AGL) ie. ASL when armed.    
  double     sonar_alt;    // cm The returned from range finder(AGL).
  double     current_lon;  // Longitude
  double     target_lon;   // Longitude I want to go to.
  double     current_lat;  // Latatude
  double     target_lat;   // Latatide I want to travil to.
  unsigned int    loiter_time;  // Time on station when drone arives.
};

struct waypoint
{
  waypoint   *next_waypoint; // Pointer to next waypoint.
  double     hold_head;      // deg. The heading to hold, this is independend of
                             //      course. 
  double     hold_alt;       // cm Altitude to hold above sea level(ASL). 
  double     target_lat;     // longitude drone needs to go to.
  double     target_lon;     // latatide drone needs to travil to.
  unsigned int   loiter_time;    // Time on station when drone arives.
};




typedef struct drone_state drone_state;
typedef struct waypoint waypoint;
