#ifndef HEADINGHOLD_SERVICE_cpp
#define HEADINGHOLD_SERVICE_cpp
#include <AutoPilot.h>
#include <HeadingHold_service.h>

extern uint8_t bit_autopilot_flags;
extern drone_state *P_state; 
//***************************************************
// This latch is what is called in the microcontroller's
// main loop. Put any required processing here
//***************************************************
void HeadingHold_service::headingHold_service_latch()
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
void HeadingHold_service::process_external_TUN_packet()
{
	// see if there is a packet waiting
	if(m_xapi.CONNECT_external_TUN_get_type() == TUN_TYPE_EXTERNAL_HEADING_HOLD)
	{	
		// allocate the space
		uint8_t TUN_packet[MED_BUFF_SZ];
		uint8_t payload_buff[SMALL_BUFF_SZ];
		uint8_t payload_buff_sz = 0;
		int hold = 0;
		// extract the packet
		m_xapi.CONNECT_external_TUN_get_packet(TUN_packet, MED_BUFF_SZ);
		
		//extract payload
		payload_buff_sz = m_util.get_TUN_payload(TUN_packet, payload_buff, SMALL_BUFF_SZ);
	
		//grab height from payload (4 bytes)
		hold = m_util.hex_to_int(0, 1, payload_buff_sz, payload_buff);
		switch(hold){
			case 0:
				//activate longitude hold
				bit_autopilot_flags |= LONGHOLD_FLAG;
				break;
			case 1: 
				//disable longitude hold
				bit_autopilot_flags &= ~LONGHOLD_FLAG;
				break;
				
			case 2:
				//activate lat hold
				bit_autopilot_flags |= LATHOLD_FLAG;
				break;
				
			case 3:
				//disable lat hold
				bit_autopilot_flags &= ~LATHOLD_FLAG;
				break;
		}

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
void HeadingHold_service::process_local_TUN_packet()
{
	// see if there is a packet waiting
	if(m_xapi.CONNECT_local_TUN_get_type() == TUN_TYPE_LOCAL_HEADING_HOLD)
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
void HeadingHold_service::reset_TUN_storage()
{
	// obsolete
}

//***************************************************
//***************************************************
HeadingHold_service::HeadingHold_service(Xapi& _xapi, LCD_service& _lcd):
m_xapi(_xapi), m_lcd(_lcd)
{
	reset_TUN_storage();

}


#endif