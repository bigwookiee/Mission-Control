#ifndef HEADING_SERVICE_cpp
#define HEADING_SERVICE_cpp
#include <AutoPilot.h>
#include <Heading_service.h>

extern uint8_t bit_autopilot_flags;
extern drone_state *P_state; 
//***************************************************
// This latch is what is called in the microcontroller's
// main loop. Put any required processing here
//***************************************************
void Heading_service::heading_service_latch()
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
void Heading_service::process_external_TUN_packet()
{
	// see if there is a packet waiting
	if(m_xapi.CONNECT_external_TUN_get_type() == TUN_TYPE_EXTERNAL_SET_HEADING)
	{	
		// allocate the space
		uint8_t TUN_packet[MED_BUFF_SZ];
		uint8_t payload_buff[SMALL_BUFF_SZ];
		uint8_t payload_buff_sz = 0;
		int heading = 0;
		// extract the packet
		m_xapi.CONNECT_external_TUN_get_packet(TUN_packet, MED_BUFF_SZ);
		
		//extract payload
		payload_buff_sz = m_util.get_TUN_payload(TUN_packet, payload_buff, SMALL_BUFF_SZ);
	
		//grab height from payload (2s bytes)
		heading = m_util.hex_to_int(0, 2, payload_buff_sz, payload_buff);
		
		P_state->hold_head = heading;
		// do something
		//lcd prints are for debugging, should be removed
		//m_lcd.lcd_print(0,0,"*************");
		//m_lcd.lcd_print(0,0,"Got Takeoff");
		//m_lcd.lcd_print(0,0,"ttest1");
		//m_lcd.lcd_print(0,0,"takeoff");		
		//m_lcd.lcd_print(0,0,"ttest2");
		//if(height = 10){
		//	m_lcd.lcd_print(0,0,"Height 10");
		//}
		//P_state->hold_alt = P_state->ground_alt + height;
		//bit_autopilot_flags |= ALTHOLD_FLAG;
	}
}

//***************************************************
//***************************************************
// This routine will query the XAPI to see
// if there is a local message waiting for
// the Land service. If so, we need to grab it and do something.
void Heading_service::process_local_TUN_packet()
{
	// see if there is a packet waiting
	if(m_xapi.CONNECT_local_TUN_get_type() == TUN_TYPE_LOCAL_SET_HEADINGs)
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
void Heading_service::reset_TUN_storage()
{
	// obsolete
}

//***************************************************
//***************************************************
Heading_service::Heading_service(Xapi& _xapi, LCD_service& _lcd):
m_xapi(_xapi), m_lcd(_lcd)
{
	reset_TUN_storage();

}


#endif