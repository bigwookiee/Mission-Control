
#ifndef SERIAL_SERVICE_cpp
#define SERIAL_SERVICE_cpp

#include <serial_service.h>

//************************************************************************
//************************************************************************
// Simply sends a buffer over serial.
// Will add a frame to the buffer
void Serial_service::snd_serial_add_frame(const uint8_t* buff, uint8_t buff_sz)
{
	m_serial.print(SENT_START_BYTE);

	m_serial.write(buff, buff_sz);
	
	m_serial.print(SENT_END_BYTE);
	
	m_serial.flush();
}

//************************************************************************
//************************************************************************
// Process any local serial messages
void Serial_service::snd_local_TUN_packet_via_serial()
{
	uint8_t packet_sz = 0;
	
	// storage for the debug packet
	uint8_t serial_packet[LARGE_BUFF_SZ];
		
	// clean packet
	m_util.clean_packet(serial_packet, LARGE_BUFF_SZ);
		
	// extract the packet
	m_xapi.CONNECT_local_TUN_get_packet(serial_packet, LARGE_BUFF_SZ);
		
	// get the size of the packet
	packet_sz = m_util.get_TUN_packet_sz(serial_packet);
		
	// ship out the packet through serial
	snd_serial_add_frame(serial_packet, packet_sz);
}

//************************************************************************
//************************************************************************
// Latch to sample the serial hardware
void Serial_service::serial_service_latch()
{
	uint8_t packet_type = 0;

	// see if there is a new byte
	if (m_serial.available() > 0) 
	{
		assemble_TUN_packet(m_serial.read());
	}
	
	// process any local serial packets that need to
	// be shipped out over serial. 
	// NOTE: the point of this code is to allow other
	// services to ship out packets via serial instead
	// of radio.
	packet_type = m_xapi.CONNECT_local_TUN_get_type();
	switch(packet_type)
	{
		case TUN_TYPE_LOCAL_SERIAL_DEBUG_MSG:
		case TUN_TYPE_LOCAL_CHAT:
			snd_local_TUN_packet_via_serial();
		break;
	}
}

//************************************************************************
//************************************************************************
// This routine allows for the serial service to display
// a simple local LCD message.
// The payload is used to create an entirely new LCD packet.
// The new derived packet is of type TUN_TYPE_LOCAL_LCD_MSG
// packet format: 
// [TYPE:2][PAYLOAD_SZ:2][CHECKSUM:4][PAYLOAD:?]
void Serial_service::simple_local_LCD_msg(	uint8_t* buff, 
											uint8_t buff_sz)
{
	// storage for payload
	uint8_t payload[MED_BUFF_SZ];
	
	// storage for new packet
	uint8_t new_TUN_packet[LARGE_BUFF_SZ];
	uint8_t new_TUN_packet_sz = 0;
	
	// extract payload
	m_util.get_TUN_payload(buff, payload, MED_BUFF_SZ);
	
	// create the TUN packet
	new_TUN_packet_sz = m_util.create_TUN_lcd_packet(	true, 0, 0, payload,
														strlen((char*)payload),
														new_TUN_packet,
														LARGE_BUFF_SZ);

	// send the new TUN packet out locally
	m_xapi.CONNECT_local_TUN_set_packet(new_TUN_packet, new_TUN_packet_sz);
}

//************************************************************************
//************************************************************************
// This routine processes a completely assembled RX TUN packet.
// Returns:
// true: packet was processed
// false: packet was not processed due to CHECKSUM error
boolean Serial_service::process_TUN_packet(	uint8_t* buff, 
											uint8_t buff_sz)
{
	boolean success = false;
	uint8_t TUN_type = ILLEGAL_TUN_TYPE;
	
	// only process the buffer if passes CHECKSUM
	if(m_util.verify_checksum(buff))
	{
		TUN_type = m_util.get_TUN_type(buff);
		
		switch(TUN_type)
		{
			// an incoming request to use the local
			// LCD screen to display a message.
			case TUN_TYPE_LOCAL_LCD_MSG:
				m_xapi.CONNECT_local_TUN_set_packet(buff, buff_sz);
			break;	
			
			// for doing a simple local LCD debug message
			case TUN_TYPE_LOCAL_SIMPLE_LCD_MSG:
				simple_local_LCD_msg(buff, buff_sz);
			break;	
			
			case TUN_TYPE_EXTERNAL_LCD_MSG:
				create_and_pass_external(TUN_TYPE_EXTERNAL_LCD_MSG, buff, buff_sz);
				//m_lcd.lcd_print("**************");
				m_lcd.lcd_print(0,0,"Got LCD");
			break;
			case TUN_TYPE_EXTERNAL_LAND:
				create_and_pass_external(TUN_TYPE_EXTERNAL_LAND,buff, buff_sz);
				//m_lcd.lcd_print("**************");
				m_lcd.lcd_print(0,0,"Got Land");
			break;
			case TUN_TYPE_EXTERNAL_TAKEOFF:
				create_and_pass_external(TUN_TYPE_EXTERNAL_TAKEOFF, buff, buff_sz);
				//m_lcd.lcd_print("**************");
				m_lcd.lcd_print(0,0,"Got Takeoff");
			break;
			case TUN_TYPE_EXTERNAL_DO_MOVE:
				create_and_pass_external(TUN_TYPE_EXTERNAL_DO_MOVE, buff, buff_sz);
				//m_lcd.lcd_print("**************");
				m_lcd.lcd_print(0,0,"Got Do Move");
			break;
		}
	
		success = true;
	}
	return success;
}

void Serial_service::create_and_pass_external(uint8_t packet_type,
											  uint8_t* buff, 
											  uint8_t buff_sz)
{
/*	Router info
					MSB: 13A200
					LSB: 40A8BC2C
				*/
				//uint32_t msb = 0x13A200;
				//uint16_t lsb = 0x40A8BC2C;
				uint32_t msb = 0x0;
				uint16_t lsb = 0xffff;
				
								
				uint8_t x = 0;
				uint8_t y = 0;
				uint8_t LCD_payload_sz = 0;
				uint8_t packet_size = 0;
				// storage for payload
				uint8_t payload[MED_BUFF_SZ];
				uint8_t message[MED_BUFF_SZ];
				// storage for new packet
				//uint8_t new_TUN_packet[LARGE_BUFF_SZ];
				//uint8_t new_TUN_packet_sz = 0;

				uint8_t payload_buff_sz = 0;
				
				uint8_t payload_buff[LARGE_BUFF_SZ];
				uint8_t packet_buff[LARGE_BUFF_SZ];

				uint8_t TUN_buff[LARGE_BUFF_SZ];
				uint8_t TUN_buff_sz = 0;					
				/*
				// extract payload, should contain x,y,message
				LCD_payload_sz = m_util.get_TUN_payload(buff, payload, MED_BUFF_SZ);

				// get the X and Y coordinates
				x = m_util.hex_to_int(LCD_X_START, LCD_X_END, LCD_X_SZ, LCD_payload);
				y = m_util.hex_to_int(LCD_Y_START, LCD_Y_END, LCD_Y_SZ, LCD_payload);
			
				// figure out the msg size
				LCD_payload_sz -= (LCD_X_SZ + LCD_Y_SZ);
			
				// display the string
				for(uint8_t i = 0; i < LCD_payload_sz; i++)
					message[i] = LCD_payload[i+LCD_MSG_START];
				*/										
				//m_lcd.lcd_print(0,0,"first");
				payload_buff_sz = m_util.get_TUN_payload(buff, payload_buff, MED_BUFF_SZ);
				//m_lcd.lcd_print(0,0,"second");

				TUN_buff_sz = m_util.create_TUN_packet(	packet_type, 
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
				
				//uint8_t* TUN_buff[3];
				//TUN_buff[0] = 'X';
				//TUN_buff[1] = 'X';
				//TUN_buff[2] = 'X';
				//lcd.lcd_snd_EXTERNAL_message(	msb,
					//					lsb,
						//				ADDR16_BROADCAST,
							//			0,
								//		0,
									//	(uint8_t*)"XXXX");
				
				//m_lcd.lcd_print(0,0,"final");
				//m_xapi.display_TUN_packet(TUN_buff, TUN_buff_sz);

}

//************************************************************************
//************************************************************************
// This routine resets the entire RX state and
// makes the service ready to RX a new TUN packet.
void Serial_service::reset_rx_state()
{
	m_util.clean_packet(m_rx_buff, LARGE_BUFF_SZ);
	m_rx_start_found = false;
	m_rx_end_found = false;
	m_rx_buff_ready = false;
	m_rx_buff_index = 0;
}

//************************************************************************
//************************************************************************
// This routine takes in RX bytes and assembles a TUN packet.
// Format of the TUNNELED (TUN) packet in ASCII-HEX:
// $[TYPE:2][PAYLOAD_SZ:2][CHECKSUM:4][PAYLOAD:?]%
uint8_t Serial_service::assemble_TUN_packet(uint8_t c)
{
	uint8_t rx_byte = c;

	// first see if a byte is waiting

		// only add to buffer is byte is read
		if(c != '\0')
		{
			// see if byte is start byte
			if(rx_byte == SENT_START_BYTE)
			{
				// record that it's the start of a new packet
				reset_rx_state();
				m_rx_start_found = true;				
			}
			else if( rx_byte == SENT_END_BYTE)
			{	
				// ensure we have read start byte
				// and there is more than 0 characters in buffer
				if( (m_rx_start_found == true) && (m_rx_buff_index > 0))
				{
					// we have a complete packet
					m_rx_end_found = true;
					m_rx_buff_ready = true;
					
					// process the packet
					process_TUN_packet(m_rx_buff, m_rx_buff_index);
					
					// reset the buffer
					reset_rx_state();
				}
			}
			else if( 	(m_rx_start_found == true) &&
						(m_rx_end_found == false) &&
						(m_rx_buff_index < (LARGE_BUFF_SZ - 1)) )
						{
							// we have a single character that is
							// not a stop or a start
							// So just store it
							m_rx_buff[m_rx_buff_index++] = rx_byte;
						}
		}
		
	// return the byte we read
	return rx_byte;
}

//************************************************
//************************************************
Serial_service::Serial_service(HardwareSerial& _serial, Xapi& _xapi, LCD_service& _lcd):
m_serial(_serial), m_xapi(_xapi), m_lcd(_lcd)
{
	m_util = Util();
	reset_rx_state();
}

#endif