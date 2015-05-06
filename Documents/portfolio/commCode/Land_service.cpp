#ifndef LAND_SERVICE_cpp
#define LAND_SERVICE_cpp
#include <Land_service.h>

//***************************************************
// This latch is what is called in the microcontroller's
// main loop. Put any required processing here
//***************************************************
void Land_service::land_service_latch()
{

	// process any local LCD message packets
	process_local_TUN_packet();
	
	// process any external LCD message packets
	process_external_TUN_packet();
}



//***************************************************
//***************************************************
// This routine will query the XAPI to see
// if there is a local message waiting for
// the Land service. If so, we need to grab it and react.
void Land_service::process_external_TUN_packet()
{
	// see if there is a packet waiting
	if(m_xapi.CONNECT_external_TUN_get_type() == TUN_TYPE_EXTERNAL_LAND)
	{	
		// allocate the space
		uint8_t TUN_packet[MED_BUFF_SZ];
	
		// extract the packet
		m_xapi.CONNECT_external_TUN_get_packet(TUN_packet, MED_BUFF_SZ);
	
		// do something
		//lcd prints are for debugging, should be removed
		//m_lcd.lcd_print("*********");
		m_lcd.lcd_print(0,0,"ltest1");
		m_lcd.lcd_print(0,0,"land");		
		m_lcd.lcd_print(0,0,"ltest2");
	}
}

//***************************************************
//***************************************************
// This routine will query the XAPI to see
// if there is a local message waiting for
// the Land service. If so, we need to grab it and do something.
void Land_service::process_local_TUN_packet()
{
	// see if there is a packet waiting
	if(m_xapi.CONNECT_local_TUN_get_type() == TUN_TYPE_LOCAL_LAND)
	{	
		// allocate the space
		uint8_t TUN_packet[MED_BUFF_SZ];
	
		// extract the packet
		m_xapi.CONNECT_local_TUN_get_packet(TUN_packet, MED_BUFF_SZ);
	
		// do something
	}
}

//***************************************************
//***************************************************
// Resets the TUN packet storage
void Land_service::reset_TUN_storage()
{
	// obsolete
}

//***************************************************
//***************************************************
Land_service::Land_service(Xapi& _xapi, LCD_service& _lcd):
m_xapi(_xapi), m_lcd(_lcd)
{
	reset_TUN_storage();

}


#endif