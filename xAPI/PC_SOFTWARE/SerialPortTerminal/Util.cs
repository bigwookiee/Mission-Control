using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace SerialPortTerminal
{
    class Util
    {
        //************************************************************************
        //************************************************************************
        // Verifies that the packet is correct
        public Boolean verify_packet(byte[] packet)
        {
            Boolean verified = false;

            if ((calculate_checksum(packet)) == get_TUN_checksum(packet))
                verified = true;

            return verified;
        }

        //************************************************************************
        //************************************************************************
        // Calculates the checksum of the packet
        // Format of the TUNNELED (TUN) packet in ASCII-HEX:
        // [TYPE:2][PAYLOAD_SZ:2][CHECKSUM:4][PAYLOAD:?]
        public uint calculate_checksum(byte[] packet)
        {
            uint checksum = 0;
            int index = 0;


            // count the first 4 bytes
            for (uint i = 0; i < (int)TUN_locations.TUN_CHECKSUM_START; i++)
                checksum += (uint)packet[index++];

            // move the index forward
            index += (int)TUN_locations.TUN_CHECKSUM_SZ;

            // calculate the payload
            for (; index < (int)BUFF_sizes.LARGE_BUFF_SZ; index++)
                checksum += (uint)packet[index];

            return checksum;
        }

        //************************************************************************
        //************************************************************************
        // Gets the TUN payload
        public int get_TUN_payload(string packet, out string str_buff)
        {
            int payload_sz = 0;
            byte[] b_buff;

            // convert to byte buffer
            byte[] b_packet = GetBytes(packet);

            // call the routine
            payload_sz = get_TUN_payload(b_packet, out b_buff);

            // convert the buffer back to a string
            str_buff = GetString(b_buff);

            return payload_sz;
        }

        //************************************************************************
        //************************************************************************
        // Gets the TUN payload
        // returns: the payload size
        public int get_TUN_payload(byte[] packet, out byte[] buff)
        {
            // get the payload size
	        int payload_sz = get_TUN_payload_sz(packet);

            // allocate space
            buff = new byte[payload_sz];
	
	        // copy the bytes over
		    for(ushort i=0; i < payload_sz; i++)
                buff[i] = packet[i + (int)TUN_locations.TUN_PAYLOAD_START];
		    			
	        return payload_sz;		
        }

        //************************************************************************
        //************************************************************************
        // Returns the TUN checksum.
        public int get_TUN_checksum(string packet)
        {
            int checksum = 0;

            // convert to byte buffer
            byte[] b_packet = GetBytes(packet);

            checksum = get_TUN_checksum(b_packet);

            return checksum;
        }

        //************************************************************************
        //************************************************************************
        // Returns the TUN checksum.
        public int get_TUN_checksum(byte[] packet)
        {
	        int checksum = 0;

            checksum = hex_to_int(  (int)TUN_locations.TUN_CHECKSUM_START,
                                    (int)TUN_locations.TUN_CHECKSUM_END,
                                    (int)TUN_locations.TUN_CHECKSUM_SZ, 
							        packet);
	        return checksum;
        }

        //*********************************************************
        //*********************************************************
        // Helper function to get TUN payload size
        public int get_TUN_payload_sz(string packet)
        {
            int payload_sz = 0;

            // convert to byte buffer
            byte[] b_packet = GetBytes(packet);

            payload_sz = get_TUN_payload_sz(b_packet);

            return payload_sz;
        }

        //*********************************************************
        //*********************************************************
        // Helper function to get TUN payload size
        public int get_TUN_payload_sz(byte[] packet)
        {
	        int payload_sz = 0;
	
	        payload_sz = hex_to_int(	(int)TUN_locations.TUN_PAYLOAD_SZ_START, 
								        (int)TUN_locations.TUN_PAYLOAD_SZ_END, 
								        (int)TUN_locations.TUN_PAYLOAD_SZ_SZ, 
								        packet);
	        return payload_sz;
        }

        //*********************************************************
        //*********************************************************
        // Helper function to create a TUN packet passing string
        public int get_TUN_type(string packet)
        {
            int type = 0;

            // convert to byte buffer
            byte[] b_packet = GetBytes(packet);

            // use the function
            type = get_TUN_type(b_packet);

            return type;
        }

        //*********************************************************
        //*********************************************************
        // Helper function to create a TUN packet passing string
        public int get_TUN_type(byte[] packet)
        {
            int type = 0;

            type = hex_to_int(  (int)TUN_locations.TUN_TYPE_START,
                                (int)TUN_locations.TUN_TYPE_END,
                                (int)TUN_locations.TUN_TYPE_SZ,
                                packet);
            return type;
        }

        //*********************************************************
        //*********************************************************
        // Helper function to create a LCD Service TUN packet passing string
        // Author: Taylor Trabun
        public int create_land_TUN_packet(int packet_type, out string buff)
        {
            int total_sz = 0;


            byte[] b_payload = {};
            byte[] b_buff;

            // do the normal call using bytes
            //total_sz = construct_lcd_payload(x, y, msg, msg.Length, out b_payload, 4 + msg.Length);

            Debug.WriteLine(LogMsgType.Outgoing, "packet payload:" + b_payload.Length + "\n");
            Debug.WriteLine(LogMsgType.Outgoing, "return size:" + total_sz + "\n");
            //total_sz = create_TUN_packet(packet_type, b_payload, out b_buff);
            total_sz = create_TUN_packet(packet_type, b_payload, out b_buff);
            Debug.WriteLine(LogMsgType.Outgoing, "packet:" + b_buff.Length + "\n");
            Debug.WriteLine(LogMsgType.Outgoing, "return size:" + total_sz + "\n");

            // convert the bytes to a string
            buff = GetString(b_buff);
            Debug.WriteLine(LogMsgType.Outgoing, "correct chcksm:" + get_TUN_checksum(buff) + "\n");
            //Debug.WriteLine(LogMsgType.Outgoing, "packet as string:" + buff + "\n");


            return total_sz;
        }
        //*********************************************************
        //*********************************************************
        // Helper function to create a LCD Service TUN packet passing string
        // Author: Taylor Trabun
        public int create_specific_move_TUN_packet(int packet_type, int moveType, int moveMetric, int moveAmount, out string buff)
        {
            int total_sz = 0;


            byte[] b_payload = new byte[8];
            byte[] b_buff;
            byte[] conv_buff_metric;
            byte[] conv_buff_type;
            byte[] conv_buff_amount;
            //2 bytes for moveType, metric, and 4 bytes for amount. Convert int to hex
            int_to_hex(moveType, (int)MISC_values.MISC_8BIT_HEX_SZ, out conv_buff_type);
            int_to_hex(moveMetric, (int)MISC_values.MISC_8BIT_HEX_SZ, out conv_buff_metric);
            int_to_hex(moveAmount, (int)MISC_values.MISC_16BIT_HEX_SZ, out conv_buff_amount);
            //create payload
            b_payload[0] = conv_buff_type[0];
            b_payload[1] = conv_buff_type[1];
            b_payload[2] = conv_buff_metric[0];
            b_payload[3] = conv_buff_metric[1];
            b_payload[4] = conv_buff_amount[0];
            b_payload[5] = conv_buff_amount[1];
            b_payload[6] = conv_buff_amount[2];
            b_payload[7] = conv_buff_amount[3];
            // do the normal call using bytes
            //total_sz = construct_lcd_payload(x, y, msg, msg.Length, out b_payload, 4 + msg.Length);

            Debug.WriteLine(LogMsgType.Outgoing, "packet payload:" + b_payload.Length + "\n");
            Debug.WriteLine(LogMsgType.Outgoing, "return size:" + total_sz + "\n");
            //total_sz = create_TUN_packet(packet_type, b_payload, out b_buff);
            total_sz = create_TUN_packet(packet_type, b_payload, out b_buff);
            Debug.WriteLine(LogMsgType.Outgoing, "packet:" + b_buff.Length + "\n");
            Debug.WriteLine(LogMsgType.Outgoing, "return size:" + total_sz + "\n");

            // convert the bytes to a string
            buff = GetString(b_buff);
            Debug.WriteLine(LogMsgType.Outgoing, "correct chcksm:" + get_TUN_checksum(buff) + "\n");
            //Debug.WriteLine(LogMsgType.Outgoing, "packet as string:" + buff + "\n");


            return total_sz;
        }
        //*********************************************************
        //*********************************************************
        // Helper function to create a takeoff Service TUN packet passing string
        // Author: Taylor Trabun
        public int create_takeoff_TUN_packet(int packet_type, int altitude, out string buff)
        {
            int total_sz = 0;


            byte[] b_payload = new byte[4];
            byte[] b_buff;
            byte[] conv_buff;

            int_to_hex(altitude, (int)MISC_values.MISC_16BIT_HEX_SZ, out conv_buff);
            b_payload[0] = conv_buff[0];
            b_payload[1] = conv_buff[1];
            b_payload[2] = conv_buff[2];
            b_payload[3] = conv_buff[3];
            // do the normal call using bytes
            //total_sz = construct_lcd_payload(x, y, msg, msg.Length, out b_payload, 4 + msg.Length);

            Debug.WriteLine(LogMsgType.Outgoing, "packet payload:" + b_payload.Length + "\n");
            Debug.WriteLine(LogMsgType.Outgoing, "return size:" + total_sz + "\n");
            //total_sz = create_TUN_packet(packet_type, b_payload, out b_buff);
            total_sz = create_TUN_packet(packet_type, b_payload, out b_buff);
            Debug.WriteLine(LogMsgType.Outgoing, "packet:" + b_buff.Length + "\n");
            Debug.WriteLine(LogMsgType.Outgoing, "return size:" + total_sz + "\n");

            // convert the bytes to a string
            buff = GetString(b_buff);
            Debug.WriteLine(LogMsgType.Outgoing, "correct chcksm:" + get_TUN_checksum(buff) + "\n");
            //Debug.WriteLine(LogMsgType.Outgoing, "packet as string:" + buff + "\n");


            return total_sz;
        }

        // Helper function to create a arm Service TUN packet passing string
        // Author: Taylor Trabun
        public int create_arm_TUN_packet(int packet_type, bool armDrone, out string buff)
        {
            int total_sz = 0;
            int armPayload;
            if(armDrone){
                armPayload = 1;
            }
            else{
                armPayload = 0;
            }

            byte[] b_payload = new byte[2];
            byte[] b_buff;
            byte[] conv_buff;

            int_to_hex(armPayload, (int)MISC_values.MISC_8BIT_HEX_SZ, out conv_buff);
            b_payload[0] = conv_buff[0];
            b_payload[1] = conv_buff[1];
            //b_payload[2] = conv_buff[2];
            //b_payload[3] = conv_buff[3];
            // do the normal call using bytes
            //total_sz = construct_lcd_payload(x, y, msg, msg.Length, out b_payload, 4 + msg.Length);

            Debug.WriteLine(LogMsgType.Outgoing, "packet payload:" + b_payload.Length + "\n");
            Debug.WriteLine(LogMsgType.Outgoing, "return size:" + total_sz + "\n");
            //total_sz = create_TUN_packet(packet_type, b_payload, out b_buff);
            total_sz = create_TUN_packet(packet_type, b_payload, out b_buff);
            Debug.WriteLine(LogMsgType.Outgoing, "packet:" + b_buff.Length + "\n");
            Debug.WriteLine(LogMsgType.Outgoing, "return size:" + total_sz + "\n");

            // convert the bytes to a string
            buff = GetString(b_buff);
            Debug.WriteLine(LogMsgType.Outgoing, "correct chcksm:" + get_TUN_checksum(buff) + "\n");
            //Debug.WriteLine(LogMsgType.Outgoing, "packet as string:" + buff + "\n");


            return total_sz;
        }


        // Helper function to create a AltHold Service TUN packet
        // Author: Taylor Trabun
        public int create_altHold_TUN_packet(int packet_type, bool setHold, out string buff)
        {
            int total_sz = 0;
            int holdPayload;
            if (setHold)
            {
                holdPayload = 1;
            }
            else
            {
                holdPayload = 0;
            }

            byte[] b_payload = new byte[2];
            byte[] b_buff;
            byte[] conv_buff;

            int_to_hex(holdPayload, (int)MISC_values.MISC_8BIT_HEX_SZ, out conv_buff);
            b_payload[0] = conv_buff[0];
            b_payload[1] = conv_buff[1];
            //b_payload[2] = conv_buff[2];
            //b_payload[3] = conv_buff[3];
            // do the normal call using bytes
            //total_sz = construct_lcd_payload(x, y, msg, msg.Length, out b_payload, 4 + msg.Length);

            Debug.WriteLine(LogMsgType.Outgoing, "packet payload:" + b_payload.Length + "\n");
            Debug.WriteLine(LogMsgType.Outgoing, "return size:" + total_sz + "\n");
            //total_sz = create_TUN_packet(packet_type, b_payload, out b_buff);
            total_sz = create_TUN_packet(packet_type, b_payload, out b_buff);
            Debug.WriteLine(LogMsgType.Outgoing, "packet:" + b_buff.Length + "\n");
            Debug.WriteLine(LogMsgType.Outgoing, "return size:" + total_sz + "\n");

            // convert the bytes to a string
            buff = GetString(b_buff);
            Debug.WriteLine(LogMsgType.Outgoing, "correct chcksm:" + get_TUN_checksum(buff) + "\n");
            //Debug.WriteLine(LogMsgType.Outgoing, "packet as string:" + buff + "\n");


            return total_sz;
        }

        // Helper function to create a Heading Service TUN packet passing int
        // Author: Taylor Trabun
        public int create_heading_TUN_packet(int packet_type, int heading, out string buff)
        {
            int total_sz = 0;

            byte[] b_payload = new byte[2];
            byte[] b_buff;
            byte[] conv_buff;

            int_to_hex(heading, (int)MISC_values.MISC_8BIT_HEX_SZ, out conv_buff);
            b_payload[0] = conv_buff[0];
            b_payload[1] = conv_buff[1];
            //b_payload[2] = conv_buff[2];
            //b_payload[3] = conv_buff[3];
            // do the normal call using bytes
            //total_sz = construct_lcd_payload(x, y, msg, msg.Length, out b_payload, 4 + msg.Length);

            Debug.WriteLine(LogMsgType.Outgoing, "packet payload:" + b_payload.Length + "\n");
            Debug.WriteLine(LogMsgType.Outgoing, "return size:" + total_sz + "\n");
            //total_sz = create_TUN_packet(packet_type, b_payload, out b_buff);
            total_sz = create_TUN_packet(packet_type, b_payload, out b_buff);
            Debug.WriteLine(LogMsgType.Outgoing, "packet:" + b_buff.Length + "\n");
            Debug.WriteLine(LogMsgType.Outgoing, "return size:" + total_sz + "\n");

            // convert the bytes to a string
            buff = GetString(b_buff);
            Debug.WriteLine(LogMsgType.Outgoing, "correct chcksm:" + get_TUN_checksum(buff) + "\n");
            //Debug.WriteLine(LogMsgType.Outgoing, "packet as string:" + buff + "\n");


            return total_sz;
        }

        //*********************************************************
        //*********************************************************
        // Helper function to create a LCD Service TUN packet passing string
        public int create_LCD_TUN_packet(int packet_type, string payload, out string buff)
        {
            int total_sz = 0;
            int x;
            int y;
            string msg;
            List<string> values = payload.Split(',').ToList<string>();
            x = Int32.Parse(values[0]);
            y = Int32.Parse(values[1]);
            msg = values[2];

            Debug.WriteLine(LogMsgType.Outgoing, "packet x:" + x + "\n");
            Debug.WriteLine(LogMsgType.Outgoing, "packet y:" + y + "\n");
            Debug.WriteLine(LogMsgType.Outgoing, "packet msg:" + msg + "\n");

            byte[] b_payload;
            byte[] b_buff;

            // do the normal call using bytes
            total_sz = construct_lcd_payload(x, y, msg, msg.Length, out b_payload, 4+msg.Length);
         
            Debug.WriteLine(LogMsgType.Outgoing, "packet payload:" + b_payload.Length + "\n");
            Debug.WriteLine(LogMsgType.Outgoing, "return size:" + total_sz + "\n");
            //total_sz = create_TUN_packet(packet_type, b_payload, out b_buff);
            total_sz = create_TUN_packet(packet_type, b_payload, out b_buff);
            Debug.WriteLine(LogMsgType.Outgoing, "packet:" + b_buff.Length + "\n");
            Debug.WriteLine(LogMsgType.Outgoing, "return size:" + total_sz + "\n");

            // convert the bytes to a string
            buff = GetString(b_buff);
            Debug.WriteLine(LogMsgType.Outgoing, "correct chcksm:" + get_TUN_checksum(buff) + "\n");
            //Debug.WriteLine(LogMsgType.Outgoing, "packet as string:" + buff + "\n");


            return total_sz;
        }
        //*********************************************************
        //*********************************************************
        // Function to create a LCD Service TUN packet passing string, x, y
        public int construct_lcd_payload(	int x, 
										int y, 
										string str, 
										int str_sz,
										out byte[] buff,
										int buff_sz)
        {
	        // storage for total size
	        int total_sz = 0;
            byte[] string_payload = GetBytes(str);
            byte[] conv_buff;
            buff = new byte[buff_sz];
	        // clean given buffer
	        

	        // tmp storage for conversions
            conv_buff = new byte[(int)BUFF_sizes.SMALL_BUFF_SZ];
	        //clean_packet(conv_buff, SMALL_BUFF_SZ);
	
	        // index into buff
	        int buff_index = 0;
	
	        // first calculate the size and ensure the 
	        // caller supplied a big enough buffer
	        total_sz += (int)MISC_values.MISC_8BIT_HEX_SZ;
	        total_sz += (int)MISC_values.MISC_8BIT_HEX_SZ;
	        total_sz += str.Length;
	
	        // only continue if buff size is big enough
	        if(total_sz <= buff_sz)
	        {
		        // convert the x and store it
		        int_to_hex(x, (int)MISC_values.MISC_8BIT_HEX_SZ, out conv_buff);
		        buff[buff_index++] = conv_buff[0];
		        buff[buff_index++] = conv_buff[1];
		
		        // convert the y and store it
		        int_to_hex(y, (int)MISC_values.MISC_8BIT_HEX_SZ, out conv_buff);
		        buff[buff_index++] = conv_buff[0];
		        buff[buff_index++] = conv_buff[1];
		
		        // store the rest of the string
		        for(int i=0; i < str_sz; i++)
			        buff[buff_index++] = string_payload[i];
	        }
	        else
		        total_sz = 0;
	
	        return total_sz;
        }

        //*********************************************************
        //*********************************************************
        // Helper function to create a TUN packet passing string
        public int create_TUN_packet(int packet_type, string payload, out string buff)
        {
            int total_sz = 0;
            byte[] b_payload = GetBytes(payload);
            byte[] b_buff;

            // do the normal call using bytes
            total_sz = create_TUN_packet(packet_type, b_payload, out b_buff);

            // convert the bytes to a string
            buff = GetString(b_buff);

            return total_sz;
        }

        //*********************************************************
        //*********************************************************
        // Helper function to create a TUN packet
        public int create_TUN_packet(int packet_type, byte[] payload, out byte[] buff)
        {
            int total_sz = 0;

            ushort checksum = 0;
            byte index = 0;

            // allocate space for the returned buffer
            buff = new byte[(int)BUFF_sizes.LARGE_BUFF_SZ];

            // extract the payload size
            int payload_sz = payload.Length;

            // temp work buffer
            byte[] tmp_buff;
	
	        // ensure the incoming buffer is large enough
	        // to hold the completed TUN packet
	        total_sz += 2; // type size
	        total_sz += 4; // checksum size
	        total_sz += 2; // payload size
	        total_sz += 2; // start and end bytes
            total_sz += payload.Length; // the size of the incoming payload
            // ensure the buffer is large enough
            if (total_sz < buff.Length)
            {
                // get the checksum of the payload
                for (int i = 0; i < payload_sz; i++)
                    checksum += payload[i];

                // place the start byte
                buff[index++] = (byte)SENT_values.SENT_START_BYTE;

                // convert the TUN packet type and store it
                int_to_hex(packet_type, (int)MISC_values.MISC_8BIT_HEX_SZ, out tmp_buff);
                buff[index++] = tmp_buff[0];
                buff[index++] = tmp_buff[1];
                checksum += tmp_buff[0];
                checksum += tmp_buff[1];

                // convert the TUN payload size and store it
                int_to_hex(payload_sz, (int)MISC_values.MISC_8BIT_HEX_SZ, out tmp_buff);
                buff[index++] = tmp_buff[0];
                buff[index++] = tmp_buff[1];
                checksum += tmp_buff[0];
                checksum += tmp_buff[1];

                // convert the checksum and store it
                int_to_hex(checksum, (int)MISC_values.MISC_16BIT_HEX_SZ, out tmp_buff);
                buff[index++] = tmp_buff[0];
                buff[index++] = tmp_buff[1];
                buff[index++] = tmp_buff[2];
                buff[index++] = tmp_buff[3];

                // place the payload into the packet
                for (int i = 0; i < payload_sz; i++)
                    buff[index++] = payload[i];

                // place the end byte
                buff[index++] = (byte)SENT_values.SENT_END_BYTE;
            }
            else
                total_sz = 0;

            return total_sz;
        }

        //*********************************************************
        //*********************************************************
        // Converts a string into a byte array
        public byte[] GetBytes(string str)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(str);
            return bytes;
        }

        //*********************************************************
        //*********************************************************
        // Converts a byte array to string
        public string GetString(byte[] bytes)
        {
            string str = Encoding.ASCII.GetString(bytes);
            return str;
        }

        //*********************************************************
        //*********************************************************
        // Converts an hex byte array into an integer
        public int hex_to_int(int start_byte, int end_byte, int size, byte[] b_array)
        {
            int value = 0;

            // convert the byte array into a string
            string str = GetString(b_array);

            // call the other routine
            value = hex_to_int(start_byte, end_byte, size, str);

            // return the value
            return value;
        }

        //*********************************************************
        //*********************************************************
        // Converts an hex string into an integer
        public int hex_to_int(int start_byte, int end_byte, int size, string str)
        {
            int value = 0;

            // get a byte array of size
            Byte[] b_array = new Byte[size];

            // convert the str into a byte array
            Byte[] str_array = GetBytes(str);

            // copy over the bytes requested
            int b_index = 0;
            for (int i = start_byte; i <= end_byte; i++)
                b_array[b_index++] = str_array[i];

            // convert the byte array to an integer
            string slice = GetString(b_array);
            try
            {
                value = (int)uint.Parse(slice, System.Globalization.NumberStyles.AllowHexSpecifier);
            }
            catch
            {
                value = 0;
            }
            // the final result as an integer
            return value;
        }

        //*********************************************************
        //*********************************************************
        // Converts an integer to a string
        public void int_to_hex(int value, int size, out string str)
        {
            // convert the string to a byte array
            byte[] b_array;

            // perform the conversion from int to hex
            int_to_hex(value, size, out b_array);

            // convert the byte array to string
            str = GetString(b_array);
        }

        //*********************************************************
        //*********************************************************
        // Converts an integer to a hex byte array
        public void int_to_hex(int value, int size, out byte[] stor_buff)
        {
            StringBuilder f_str = new StringBuilder("{0:X");
            f_str.Append(size.ToString());
            f_str.Append("}");

            string f = string.Format(f_str.ToString(), value);
            stor_buff = GetBytes(f);   
        }

        //*********************************************************
        //*********************************************************
        // Constructor
        public Util()
        {
       
        }
        

    }
}
