#ifndef ALTHOLD_SERVICE_h
#define ALTHOLD_SERVICE_h
#include <arduino.h>
#include <Xapi.h>
#include <Util.h>
#include <LCD_service.h>


//**********************************************
//**********************************************

class AltHold_service
{
	// objects used
	private:
		Xapi& m_xapi;
		Util m_util;
		LCD_service& m_lcd;
	private:

		
	// functions for althold service
	private:
		void reset_TUN_storage();
		void process_local_TUN_packet();
		void process_external_TUN_packet();
		
	// general functions for althold
	public:
		void altHold_service_latch();
		
	// Constructor
	public:
		AltHold_service(Xapi& _xapi, LCD_service& _lcd);
};

#endif