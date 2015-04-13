//#ifndef AutoPilot_H
//#define AutoPilot_H

/******************************
Version 1.6  


4/03/2015

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

version history
4/03/2015 1.6
First attempted at berring hold.




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

/* Bitflag used to indecate autopilot status.
This ius a power of 2. valied numbers are
1,2,4,8,16,32,64,128
0x01 0x02 0x04 0x08 0x10 0x20 0x40 0x80
0b00000001 0b00000010 ... 0b10000000
*/
#define HEADING_FLAG 	1
#define ALTHOLD_FLAG 	2
#define IS_ARMED_FLAG	64
#define IS_ARMED_FLAG_POSITION 6
#define ARM_FLAG     	128
#define ARM_FLAG_POSITION 7

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
