#ifndef ARM_SERVICE_cpp
#define ARM_SERVICE_cpp
#include <Arm_service.h>
#include <AutoPilot.h>

extern uint8_t bit_autopilot_flags;
//***************************************************
// This latch is what is called in the microcontroller's
// main loop. Put any required processing here
//***************************************************
void Arm_service::arm_service_latch()
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
void Arm_service::process_external_TUN_packet()
{
	// see if there is a packet waiting
	if(m_xapi.CONNECT_external_TUN_get_type() == TUN_TYPE_EXTERNAL_ARM)
	{	
		// allocate the space
		uint8_t TUN_packet[MED_BUFF_SZ];
	
		// extract the packet
		m_xapi.CONNECT_external_TUN_get_packet(TUN_packet, MED_BUFF_SZ);
	
		// do something
		//lcd prints are for debugging, should be removed
		//m_lcd.lcd_print("*********");
		//m_lcd.lcd_print(0,0,"ltest1");
		//m_lcd.lcd_print(0,0,"arm packet");
		Serial.print("I should be armed AutoPilot: ");

		bit_autopilot_flags |= ARM_FLAG;  // Turns bir on in flags
		Serial.println(bit_autopilot_flags);
		// Turns Turns off Arm --->>   bit_autopilot_flags &= ~ARM_FLAG;
		



		//m_lcd.lcd_print(0,0,"ltest2");
	}
}

//***************************************************
//***************************************************
// This routine will query the XAPI to see
// if there is a local message waiting for
// the Land service. If so, we need to grab it and do something.
void Arm_service::process_local_TUN_packet()
{
	// see if there is a packet waiting
	if(m_xapi.CONNECT_local_TUN_get_type() == TUN_TYPE_LOCAL_ARM)
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
void Arm_service::reset_TUN_storage()
{
	// obsolete
}

//***************************************************
//***************************************************
Arm_service::Arm_service(Xapi& _xapi, LCD_service& _lcd):
m_xapi(_xapi), m_lcd(_lcd)
{
	reset_TUN_storage();

}


#endif