#ifndef LAND_SERVICE_h
#define LAND_SERVICE_h
#include <arduino.h>
#include <Xapi.h>
#include <Util.h>
#include <LCD_service.h>


//**********************************************
//**********************************************

class Land_service
{
	// objects used
	private:
		Xapi& m_xapi;
		Util m_util;
		LCD_service& m_lcd;
		
	private:

		
	// functions for land service
	private:
		void reset_TUN_storage();
		void process_local_TUN_packet();
		void process_external_TUN_packet();
		
	// general functions for landing
	public:
		void land_service_latch();
		
	// Constructor
	public:
		Land_service(Xapi& _xapi, LCD_service& _lcd);
};

#endif