#ifndef HEARTBEAT_SERVICE_h
#define HEARTBEAT_SERVICE_h
#include <arduino.h>
#include <Xapi.h>
#include <Util.h>
#include <LCD_service.h>


//**********************************************
//**********************************************

class Heartbeat_service
{
	// objects used
	private:
		Xapi& m_xapi;
		Util m_util;
		LCD_service& m_lcd;
		unsigned long lastSent;
		
	private:

		
	// functions for land service
	private:
		void reset_TUN_storage();
		void process_local_TUN_packet();
		void process_external_TUN_packet();
		void send_heartbeat();
		
	// general functions for landing
	public:
		void heartbeat_service_latch();
		
	// Constructor
	public:
		Heartbeat_service(Xapi& _xapi, LCD_service& _lcd);
};

#endif