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

//0013a200
//40a1446d

//***********************************************
//***********************************************
Xapi xapi = Xapi(Serial);
//Serial_service serial_service = Serial_service(Serial1, xapi);
LCD_service lcd_service (xapi);
//************************************************
// The next services pertain to drone instructions
//************************************************
Takeoff_service takeoff_service(xapi, lcd_service);
Land_service land_service(xapi, lcd_service);
//Serial_service serial_service = Serial_service(Serial1, xapi, lcd_service);
uint8_t msg1[] =   "I FEEL GREAT";
uint8_t msg2[] =   "COMMODORE 64";
uint8_t _clear[] = "                ";


//***********************************************
//***********************************************
void setup()
{
 Serial.begin(MISC_PC_SPEED);
 //Serial1.begin(MISC_PC_SPEED);
  
  
  
}

//***********************************************
//***********************************************
void loop()
{
  //system_active();
  //process_buttons();
  xapi.xapi_latch();
  lcd_service.lcd_service_latch();
  takeoff_service.takeoff_service_latch();
  land_service.land_service_latch();
  //serial_service.serial_service_latch();
  
  //delay(4000);
}


//***********************************************
// Old function. Not needed
//***********************************************
void system_active()
{
    static uint16_t cnt = 0;
    static uint8_t row = 0;
    
    cnt++;
    
    if ( (cnt%2500) == 0)
   {
     // turn off both stars
     lcd_service.lcd_print(15, 0, (const char*)" ");
     lcd_service.lcd_print(15, 1, (const char*)" ");
     
     row++;
     // turn on new row
     lcd_service.lcd_print(15, row%2, (const char*)"*");
    
   } 
  
}





