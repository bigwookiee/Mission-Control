#ifndef HEARTBEAT_SERVICE_cpp
#define HEARBEAT_SERVICE_cpp
#include <Heartbeat_service.h>

//***************************************************
// This latch is what is called in the microcontroller's
// main loop. Put any required processing here
//***************************************************
void Heartbeat_service::heartbeat_service_latch()
{
	unsigned long currTime = millis();
	// process any local LCD message packets
	process_local_TUN_packet();
	
	// process any external LCD message packets
	process_external_TUN_packet();
	//millis() will rollover to 0 after 50 days, extra provisioning
	if(currTime < lastSent){
		lastSent = currTime;
	}
	//if 1 second has passed send heartbeat
	if((currTime-lastSent) > 1000){
		send_heartbeat();
		lastSent = millis();
	}
}



//***************************************************
//***************************************************
// This routine will query the XAPI to see
// if there is a local message waiting for
// the Land service. If so, we need to grab it and react.
void Heartbeat_service::process_external_TUN_packet()
{
	// see if there is a packet waiting
	if(m_xapi.CONNECT_external_TUN_get_type() == TUN_TYPE_EXTERNAL_HEARTBEAT)
	{	
		// allocate the space
		uint8_t TUN_packet[MED_BUFF_SZ];
	
		// extract the packet
		m_xapi.CONNECT_external_TUN_get_packet(TUN_packet, MED_BUFF_SZ);
	
		// do something
		//lcd prints are for debugging, should be removed
		//m_lcd.lcd_print("*********");
		//m_lcd.lcd_print(0,0,"ltest1");
		m_lcd.lcd_print(0,0,"heartbeat packet");		
		//m_lcd.lcd_print(0,0,"ltest2");
	}
}

//***************************************************
//***************************************************
// This routine will query the XAPI to see
// if there is a local message waiting for
// the Land service. If so, we need to grab it and do something.
void Heartbeat_service::process_local_TUN_packet()
{
	// see if there is a packet waiting
	if(m_xapi.CONNECT_local_TUN_get_type() == TUN_TYPE_LOCAL_HEARTBEAT)
	{	
		// allocate the space
		uint8_t TUN_packet[MED_BUFF_SZ];
	
		// extract the packet
		m_xapi.CONNECT_local_TUN_get_packet(TUN_packet, MED_BUFF_SZ);
	
		// do something
	}
}
//**************************************************
//**************************************************
// This function is used to send a external heartbeat 
// packet, which contains drone information
void Heartbeat_service::send_heartbeat()
{
	uint32_t msb = 0x0;
	uint16_t lsb = 0xffff;
	
					
	uint8_t x = 0;
	uint8_t y = 0;
	uint8_t LCD_payload_sz = 0;
	uint8_t packet_size = 0;
	// storage for payload
	//uint8_t payload[] = {0x00};
	//uint8_t message[MED_BUFF_SZ];
	// storage for new packet
	//uint8_t new_TUN_packet[LARGE_BUFF_SZ];
	//uint8_t new_TUN_packet_sz = 0;

	uint8_t payload_buff_sz = 0;
	
	uint8_t payload_buff[MED_BUFF_SZ];
	uint8_t packet_buff[LARGE_BUFF_SZ];

	uint8_t TUN_buff[LARGE_BUFF_SZ];
	uint8_t TUN_buff_sz = 0;					

		
	//m_lcd.lcd_print(0,0,"first");
	//payload_buff_sz = m_util.get_TUN_payload(buff, payload_buff, MED_BUFF_SZ);
	payload_buff[0] = 0x00; //temp code
	payload_buff_sz = 2;
	//m_lcd.lcd_print(0,0,"second");

	TUN_buff_sz = m_util.create_TUN_packet(	TUN_TYPE_EXTERNAL_HEARTBEAT, 
							payload_buff, 
							payload_buff_sz, 
							TUN_buff, 
							LARGE_BUFF_SZ);
	//construct and send packet
					//m_lcd.lcd_print(0,0,"third");

					
	//TUN_buff[0] = 'X';
	//TUN_buff[1] = 'X';
	//TUN_buff[2] = 'X';
	//TUN_buff_sz = 3;
	
	packet_size = m_xapi.construct_transmit_req(msb, 
												lsb, 
												ADDR16_BROADCAST,
												TUN_buff, 
												TUN_buff_sz,
												packet_buff, 
												LARGE_BUFF_SZ);
	//m_lcd.lcd_print(0,0,"fourth");


	m_xapi.snd_packet(packet_buff, packet_size);

}

//***************************************************
//***************************************************
// Resets the TUN packet storage
void Heartbeat_service::reset_TUN_storage()
{
	// obsolete
}

//***************************************************
//***************************************************
Heartbeat_service::Heartbeat_service(Xapi& _xapi, LCD_service& _lcd):
m_xapi(_xapi), m_lcd(_lcd)
{
	reset_TUN_storage();
	//send first heartbeat
	send_heartbeat();
	//initialize lastSend
	lastSent = millis();

}


#endif