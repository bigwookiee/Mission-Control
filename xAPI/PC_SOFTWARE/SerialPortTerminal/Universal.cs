using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SerialPortTerminal
{
    //***********************************************
    //***********************************************
    // standard buffer sizes
    enum BUFF_sizes 
    { 
        LARGE_BUFF_SZ = 70, MED_BUFF_SZ = 40, SMALL_BUFF_SZ = 20
    }

    //***********************************************
    //***********************************************
    // misc values
    enum MISC_values
    {
        MISC_SERIAL_SPEED = 57600, MISC_8BIT_HEX_SZ = 2, MISC_16BIT_HEX_SZ = 4,
        MISC_32BIT_HEX_SZ = 8, MISC_ANTI_SLAM = 220, CHECKSUM_ERROR = -1
    }

    //***********************************************
    //***********************************************
    // Locations into the TUN type necessary for 
    // extractions
    // Format of the TUNNELED (TUN) packet in ASCII-HEX:
    // [TYPE:2][PAYLOAD_SZ:2][CHECKSUM:4][PAYLOAD:?]
    enum TUN_locations 
    {
        TUN_TYPE_START=0, TUN_TYPE_END = 1, TUN_TYPE_SZ = 2,
        TUN_PAYLOAD_SZ_START=2, TUN_PAYLOAD_SZ_END=3, TUN_PAYLOAD_SZ_SZ=2,
        TUN_CHECKSUM_START=4, TUN_CHECKSUM_END=7, TUN_CHECKSUM_SZ=4,
        TUN_PAYLOAD_START=8
    }
   
    //***********************************************
    //***********************************************
    // Addresses used for broadcasts
    enum ADDR_bc
    {
        MSB_BROADCAST_ADDR=0x00000000, LSB_BROADCAST_ADDR=0x0000FFFF,
        ADDR16_BROADCAST=0xFFFE
    }

    //***********************************************
    //***********************************************
    // Sentinel bytes (SENT) used throughout the system.
    enum SENT_values
    {
        SENT_START_BYTE='$', SENT_END_BYTE='%', SENT_API_NULL_BYTE=0x00,
        SENT_CHAR_NULL_BYTE='\0'
    }

    //***********************************************
    //***********************************************
    // Movement direction values for drone instruction
    enum DRONE_movement_dir
    {
        MOVE_FORWARD = 0x00, MOVE_BACKWARD = 0x01,
        MOVE_LEFT = 0x02, MOVE_RIGHT = 0x03, MOVE_ROTATE_CLOCKWISE = 0x04
    }

    //***********************************************
    //***********************************************
    // Movement metric types for drone instruction
    enum DRONE_movement_metric
    {
        METRIC_FEET = 0x00, METRIC_SECONDS = 0x01, METRIC_METERS = 0x02,
        METRIC_DEGREES = 0x03
    }


    //***********************************************
    //***********************************************
    // TUN packet types
    enum TUN_types
    {
        ILLEGAL_TUN_TYPE = 0x00, TUN_TYPE_EXTERNAL_LCD_MSG = 0x01, 
        TUN_TYPE_LOCAL_LCD_MSG = 0x02, TUN_TYPE_LOCAL_SIMPLE_LCD_MSG = 0x03,
        TUN_TYPE_CHAT = 0x05, TUN_TYPE_LOCAL_TAKEOFF = 0x50, TUN_TYPE_EXTERNAL_TAKEOFF = 0x51, 
        TUN_TYPE_LOCAL_LAND = 0x52, TUN_TYPE_EXTERNAL_LAND = 0x53, 
        TUN_TYPE_LOCAL_GOTO_COORD = 0x54, TUN_TYPE_EXTERNAL_GOTO_COORD = 0x55, 
        TUN_TYPE_LOCAL_DO_MOVE = 0x56, TUN_TYPE_EXTERNAL_DO_MOVE = 0x57, 
        //TUN_TYPE_LOCAL_ROTATE = 0x5A, TUN_TYPE_EXTERNAL_ROTATE = 0x5B,
        TUN_TYPE_LOCAL_UPLOAD_START = 0x58, TUN_TYPE_EXTERNAL_UPLOAD_START = 0x59, 
        TUN_TYPE_LOCAL_UPLOAD_INSTRUCTION_COORD = 0x60, TUN_TYPE_EXTERNAL_UPLOAD_INSTRUCTION_COORD = 0x61, 
        TUN_TYPE_LOCAL_UPLOAD_INSTRUCTION_DO_MOVE = 0x62, TUN_TYPE_EXTERNAL_UPLOAD_INSTRUCTION_DO_MOVE = 0x63, 
        TUN_TYPE_LOCAL_UPLOAD_INSTRUCTION_LAND = 0x6A, TUN_TYPE_EXTERNAL_UPLOAD_INSTRUCTION_LAND = 0x6B,
        TUN_TYPE_LOCAL_UPLOAD_INSTRUCTION_TAKEOFF = 0x6C,
        TUN_TYPE_EXTERNAL_UPLOAD_INSTRUCTION_TAKEOFF = 0x6D, //TUN_TYPE_LOCAL_UPLOAD_INSTRUCTION_ROTATE = 0x6E,
        //TUN_TYPE_EXTERNAL_UPLOAD_INSTRUCTION_ROTATE = 0x6F,
        TUN_TYPE_LOCAL_UPLOAD_END = 0x64, TUN_TYPE_EXTERNAL_UPLOAD_END = 0x65, 
        TUN_TYPE_LOCAL_MANUAL_OVERRIDE_MAINTAIN_ALT = 0x66, TUN_TYPE_EXTERNAL_MANUAL_OVERRIDE_MAINTAIN_ALT = 0x67, 
        TUN_TYPE_LOCAL_MANUAL_OVERRIDE_FULL = 0x68, TUN_TYPE_EXTERNAL_MANUAL_OVERRIDE_FULL = 0x69, 
        TUN_TYPE_LOCAL_ACK = 0x70, TUN_TYPE_EXTERNAL_ACK = 0x71, TUN_TYPE_LOCAL_NACK = 0x72, 
        TUN_TYPE_EXTERNAL_NACK = 0x73, TUN_TYPE_LOCAL_HEARTBEAT = 0x74, 
        TUN_TYPE_EXTERNAL_HEARTBEAT = 0x75, TUN_TYPE_LOCAL_SET_ALTITUDE = 0x76, 
        TUN_TYPE_EXTERNAL_SET_ALTITUDE = 0x77, TUN_TYPE_LOCAL_SET_NEVER_EXCEED_SPEED = 0x78, 
        TUN_TYPE_EXTERNAL_SET_NEVER_EXCEED_SPEED = 0x79, TUN_TYPE_LOCAL_SET_NEVER_EXCEED_FALL_SPEED = 0x80, 
        TUN_TYPE_EXTERNAL_SET_NEVER_EXCEED_FALL_SPEED = 0x81, TUN_TYPE_LOCAL_SET_THROTTLE = 0x82, 
        TUN_TYPE_EXTERNAL_SET_THROTTLE = 0x83,
        TUN_TYPE_LOCAL_ARM = 0x90, TUN_TYPE_EXTERNAL_ARM = 0x91, 
        TUN_TYPE_LOCAL_SET_HEADING = 0x92, TUN_TYPE_EXTERNAL_SET_HEADING = 0x93,
        TUN_TYPE_LOCAL_ALT_HOLD = 0x94, TUN_TYPE_EXTERNAL_ALT_HOLD = 0x95,
        TUN_TYPE_LOCAL_HEADING_HOLD = 0x96, TUN_TYPE_EXTERNAL_HEADING_HOLD = 0x97
    }
 

}
