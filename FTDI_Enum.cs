using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;

namespace FTD2XX_NET
{
    /// <summary>
    /// Status values for FTDI devices.
    /// </summary>
    ///
    public partial class FTDI
    {
        public enum FT_STATUS
        {
            /// <summary>
            /// Status OK
            /// </summary>
            FT_OK,
            /// <summary>
            /// The device handle is invalid
            /// </summary>
            FT_INVALID_HANDLE,
            /// <summary>
            /// Device not found
            /// </summary>
            FT_DEVICE_NOT_FOUND,
            /// <summary>
            /// Device is not open
            /// </summary>
            FT_DEVICE_NOT_OPENED,
            /// <summary>
            /// IO error
            /// </summary>
            FT_IO_ERROR,
            /// <summary>
            /// Insufficient resources
            /// </summary>
            FT_INSUFFICIENT_RESOURCES,
            /// <summary>
            /// A parameter was invalid
            /// </summary>
            FT_INVALID_PARAMETER,
            /// <summary>
            /// The requested baud rate is invalid
            /// </summary>
            FT_INVALID_BAUD_RATE,
            /// <summary>
            /// Device not opened for erase
            /// </summary>
            FT_DEVICE_NOT_OPENED_FOR_ERASE,
            /// <summary>
            /// Device not poened for write
            /// </summary>
            FT_DEVICE_NOT_OPENED_FOR_WRITE,
            /// <summary>
            /// Failed to write to device
            /// </summary>
            FT_FAILED_TO_WRITE_DEVICE,
            /// <summary>
            /// Failed to read the device EEPROM
            /// </summary>
            FT_EEPROM_READ_FAILED,
            /// <summary>
            /// Failed to write the device EEPROM
            /// </summary>
            FT_EEPROM_WRITE_FAILED,
            /// <summary>
            /// Failed to erase the device EEPROM
            /// </summary>
            FT_EEPROM_ERASE_FAILED,
            /// <summary>
            /// An EEPROM is not fitted to the device
            /// </summary>
            FT_EEPROM_NOT_PRESENT,
            /// <summary>
            /// Device EEPROM is blank
            /// </summary>
            FT_EEPROM_NOT_PROGRAMMED,
            /// <summary>
            /// Invalid arguments
            /// </summary>
            FT_INVALID_ARGS,
            /// <summary>
            /// An other error has occurred
            /// </summary>
            FT_OTHER_ERROR
        }

        /// <summary>
        /// Error states not supported by FTD2XX DLL.
        /// </summary>
        private enum FT_ERROR
        {
            FT_NO_ERROR,
            FT_INCORRECT_DEVICE,
            FT_INVALID_BITMODE,
            FT_BUFFER_SIZE
        }

        /// <summary>
        /// Permitted data bits for FTDI devices
        /// </summary>
        public class FT_DATA_BITS
        {
            /// <summary>
            /// 8 data bits
            /// </summary>
            public const byte FT_BITS_8 = 8;

            /// <summary>
            /// 7 data bits
            /// </summary>
            public const byte FT_BITS_7 = 7;
        }

        /// <summary>
        /// Permitted stop bits for FTDI devices
        /// </summary>
        public class FT_STOP_BITS
        {
            /// <summary>
            /// 1 stop bit
            /// </summary>
            public const byte FT_STOP_BITS_1 = 0;

            /// <summary>
            /// 2 stop bits
            /// </summary>
            public const byte FT_STOP_BITS_2 = 2;
        }

        /// <summary>
        /// Permitted parity values for FTDI devices
        /// </summary>
        public class FT_PARITY
        {
            /// <summary>
            /// No parity
            /// </summary>
            public const byte FT_PARITY_NONE = 0;

            /// <summary>
            /// Odd parity
            /// </summary>
            public const byte FT_PARITY_ODD = 1;

            /// <summary>
            /// Even parity
            /// </summary>
            public const byte FT_PARITY_EVEN = 2;

            /// <summary>
            /// Mark parity
            /// </summary>
            public const byte FT_PARITY_MARK = 3;

            /// <summary>
            /// Space parity
            /// </summary>
            public const byte FT_PARITY_SPACE = 4;
        }

        /// <summary>
        /// Permitted flow control values for FTDI devices
        /// </summary>
        public class FT_FLOW_CONTROL
        {
            /// <summary>
            /// No flow control
            /// </summary>
            public const ushort FT_FLOW_NONE = 0;

            /// <summary>
            /// RTS/CTS flow control
            /// </summary>
            public const ushort FT_FLOW_RTS_CTS = 256;

            /// <summary>
            /// DTR/DSR flow control
            /// </summary>
            public const ushort FT_FLOW_DTR_DSR = 512;

            /// <summary>
            /// Xon/Xoff flow control
            /// </summary>
            public const ushort FT_FLOW_XON_XOFF = 1024;
        }

        /// <summary>
        /// Purge buffer constant definitions
        /// </summary>
        public static class FT_PURGE
        {
            /// <summary>
            /// Purge Rx buffer
            /// </summary>
            public const byte FT_PURGE_RX = 1;

            /// <summary>
            /// Purge Tx buffer
            /// </summary>
            public const byte FT_PURGE_TX = 2;
        }

        /// <summary>
        /// Modem status bit definitions
        /// </summary>
        public static class FT_MODEM_STATUS
        {
            /// <summary>
            /// Clear To Send (CTS) modem status
            /// </summary>
            public const byte FT_CTS = 16;

            /// <summary>
            /// Data Set Ready (DSR) modem status
            /// </summary>
            public const byte FT_DSR = 32;

            /// <summary>
            /// Ring Indicator (RI) modem status
            /// </summary>
            public const byte FT_RI = 64;

            /// <summary>
            /// Data Carrier Detect (DCD) modem status
            /// </summary>
            public const byte FT_DCD = 128;
        }

        /// <summary>
        /// Line status bit definitions
        /// </summary>
        public static class FT_LINE_STATUS
        {
            /// <summary>
            /// Overrun Error (OE) line status
            /// </summary>
            public const byte FT_OE = 2;

            /// <summary>
            /// Parity Error (PE) line status
            /// </summary>
            public const byte FT_PE = 4;

            /// <summary>
            /// Framing Error (FE) line status
            /// </summary>
            public const byte FT_FE = 8;

            /// <summary>
            /// Break Interrupt (BI) line status
            /// </summary>
            public const byte FT_BI = 16;
        }

        /// <summary>
        /// FTDI device event types that can be monitored
        /// </summary>
        public class FT_EVENTS
        {
            /// <summary>
            /// Event on receive character
            /// </summary>
            public const uint FT_EVENT_RXCHAR = 1u;

            /// <summary>
            /// Event on modem status change
            /// </summary>
            public const uint FT_EVENT_MODEM_STATUS = 2u;

            /// <summary>
            /// Event on line status change
            /// </summary>
            public const uint FT_EVENT_LINE_STATUS = 4u;
        }

        /// <summary>
        /// Permitted bit mode values for FTDI devices.  For use with SetBitMode
        /// </summary>
        public class FT_BIT_MODES
        {
            /// <summary>
            /// Reset bit mode
            /// </summary>
            public const byte FT_BIT_MODE_RESET = 0;

            /// <summary>
            /// Asynchronous bit-bang mode
            /// </summary>
            public const byte FT_BIT_MODE_ASYNC_BITBANG = 1;

            /// <summary>
            /// MPSSE bit mode - only available on FT2232, FT2232H, FT4232H and FT232H
            /// </summary>
            public const byte FT_BIT_MODE_MPSSE = 2;

            /// <summary>
            /// Synchronous bit-bang mode
            /// </summary>
            public const byte FT_BIT_MODE_SYNC_BITBANG = 4;

            /// <summary>
            /// MCU host bus emulation mode - only available on FT2232, FT2232H, FT4232H and FT232H
            /// </summary>
            public const byte FT_BIT_MODE_MCU_HOST = 8;

            /// <summary>
            /// Fast opto-isolated serial mode - only available on FT2232, FT2232H, FT4232H and FT232H
            /// </summary>
            public const byte FT_BIT_MODE_FAST_SERIAL = 16;

            /// <summary>
            /// CBUS bit-bang mode - only available on FT232R and FT232H
            /// </summary>
            public const byte FT_BIT_MODE_CBUS_BITBANG = 32;

            /// <summary>
            /// Single channel synchronous 245 FIFO mode - only available on FT2232H channel A and FT232H
            /// </summary>
            public const byte FT_BIT_MODE_SYNC_FIFO = 64;
        }

        /// <summary>
        /// Available functions for the FT232R CBUS pins.  Controlled by FT232R EEPROM settings
        /// </summary>
        public class FT_CBUS_OPTIONS
        {
            /// <summary>
            /// FT232R CBUS EEPROM options - Tx Data Enable
            /// </summary>
            public const byte FT_CBUS_TXDEN = 0;

            /// <summary>
            /// FT232R CBUS EEPROM options - Power On
            /// </summary>
            public const byte FT_CBUS_PWRON = 1;

            /// <summary>
            /// FT232R CBUS EEPROM options - Rx LED
            /// </summary>
            public const byte FT_CBUS_RXLED = 2;

            /// <summary>
            /// FT232R CBUS EEPROM options - Tx LED
            /// </summary>
            public const byte FT_CBUS_TXLED = 3;

            /// <summary>
            /// FT232R CBUS EEPROM options - Tx and Rx LED
            /// </summary>
            public const byte FT_CBUS_TXRXLED = 4;

            /// <summary>
            /// FT232R CBUS EEPROM options - Sleep
            /// </summary>
            public const byte FT_CBUS_SLEEP = 5;

            /// <summary>
            /// FT232R CBUS EEPROM options - 48MHz clock
            /// </summary>
            public const byte FT_CBUS_CLK48 = 6;

            /// <summary>
            /// FT232R CBUS EEPROM options - 24MHz clock
            /// </summary>
            public const byte FT_CBUS_CLK24 = 7;

            /// <summary>
            /// FT232R CBUS EEPROM options - 12MHz clock
            /// </summary>
            public const byte FT_CBUS_CLK12 = 8;

            /// <summary>
            /// FT232R CBUS EEPROM options - 6MHz clock
            /// </summary>
            public const byte FT_CBUS_CLK6 = 9;

            /// <summary>
            /// FT232R CBUS EEPROM options - IO mode
            /// </summary>
            public const byte FT_CBUS_IOMODE = 10;

            /// <summary>
            /// FT232R CBUS EEPROM options - Bit-bang write strobe
            /// </summary>
            public const byte FT_CBUS_BITBANG_WR = 11;

            /// <summary>
            /// FT232R CBUS EEPROM options - Bit-bang read strobe
            /// </summary>
            public const byte FT_CBUS_BITBANG_RD = 12;
        }

        /// <summary>
        /// Available functions for the FT232H CBUS pins.  Controlled by FT232H EEPROM settings
        /// </summary>
        public class FT_232H_CBUS_OPTIONS
        {
            /// <summary>
            /// FT232H CBUS EEPROM options - Tristate
            /// </summary>
            public const byte FT_CBUS_TRISTATE = 0;

            /// <summary>
            /// FT232H CBUS EEPROM options - Rx LED
            /// </summary>
            public const byte FT_CBUS_RXLED = 1;

            /// <summary>
            /// FT232H CBUS EEPROM options - Tx LED
            /// </summary>
            public const byte FT_CBUS_TXLED = 2;

            /// <summary>
            /// FT232H CBUS EEPROM options - Tx and Rx LED
            /// </summary>
            public const byte FT_CBUS_TXRXLED = 3;

            /// <summary>
            /// FT232H CBUS EEPROM options - Power Enable#
            /// </summary>
            public const byte FT_CBUS_PWREN = 4;

            /// <summary>
            /// FT232H CBUS EEPROM options - Sleep
            /// </summary>
            public const byte FT_CBUS_SLEEP = 5;

            /// <summary>
            /// FT232H CBUS EEPROM options - Drive pin to logic 0
            /// </summary>
            public const byte FT_CBUS_DRIVE_0 = 6;

            /// <summary>
            /// FT232H CBUS EEPROM options - Drive pin to logic 1
            /// </summary>
            public const byte FT_CBUS_DRIVE_1 = 7;

            /// <summary>
            /// FT232H CBUS EEPROM options - IO Mode
            /// </summary>
            public const byte FT_CBUS_IOMODE = 8;

            /// <summary>
            /// FT232H CBUS EEPROM options - Tx Data Enable
            /// </summary>
            public const byte FT_CBUS_TXDEN = 9;

            /// <summary>
            /// FT232H CBUS EEPROM options - 30MHz clock
            /// </summary>
            public const byte FT_CBUS_CLK30 = 10;

            /// <summary>
            /// FT232H CBUS EEPROM options - 15MHz clock
            /// </summary>
            public const byte FT_CBUS_CLK15 = 11;

            /// <summary>
            /// FT232H CBUS EEPROM options - 7.5MHz clock
            /// </summary>
            public const byte FT_CBUS_CLK7_5 = 12;
        }

        /// <summary>
        /// Available functions for the X-Series CBUS pins.  Controlled by X-Series EEPROM settings
        /// </summary>
        public class FT_XSERIES_CBUS_OPTIONS
        {
            /// <summary>
            /// FT X-Series CBUS EEPROM options - Tristate
            /// </summary>
            public const byte FT_CBUS_TRISTATE = 0;

            /// <summary>
            /// FT X-Series CBUS EEPROM options - RxLED#
            /// </summary>
            public const byte FT_CBUS_RXLED = 1;

            /// <summary>
            /// FT X-Series CBUS EEPROM options - TxLED#
            /// </summary>
            public const byte FT_CBUS_TXLED = 2;

            /// <summary>
            /// FT X-Series CBUS EEPROM options - TxRxLED#
            /// </summary>
            public const byte FT_CBUS_TXRXLED = 3;

            /// <summary>
            /// FT X-Series CBUS EEPROM options - PwrEn#
            /// </summary>
            public const byte FT_CBUS_PWREN = 4;

            /// <summary>
            /// FT X-Series CBUS EEPROM options - Sleep#
            /// </summary>
            public const byte FT_CBUS_SLEEP = 5;

            /// <summary>
            /// FT X-Series CBUS EEPROM options - Drive_0
            /// </summary>
            public const byte FT_CBUS_Drive_0 = 6;

            /// <summary>
            /// FT X-Series CBUS EEPROM options - Drive_1
            /// </summary>
            public const byte FT_CBUS_Drive_1 = 7;

            /// <summary>
            /// FT X-Series CBUS EEPROM options - GPIO
            /// </summary>
            public const byte FT_CBUS_GPIO = 8;

            /// <summary>
            /// FT X-Series CBUS EEPROM options - TxdEn
            /// </summary>
            public const byte FT_CBUS_TXDEN = 9;

            /// <summary>
            /// FT X-Series CBUS EEPROM options - Clk24MHz
            /// </summary>
            public const byte FT_CBUS_CLK24MHz = 10;

            /// <summary>
            /// FT X-Series CBUS EEPROM options - Clk12MHz
            /// </summary>
            public const byte FT_CBUS_CLK12MHz = 11;

            /// <summary>
            /// FT X-Series CBUS EEPROM options - Clk6MHz
            /// </summary>
            public const byte FT_CBUS_CLK6MHz = 12;

            /// <summary>
            /// FT X-Series CBUS EEPROM options - BCD_Charger
            /// </summary>
            public const byte FT_CBUS_BCD_Charger = 13;

            /// <summary>
            /// FT X-Series CBUS EEPROM options - BCD_Charger#
            /// </summary>
            public const byte FT_CBUS_BCD_Charger_N = 14;

            /// <summary>
            /// FT X-Series CBUS EEPROM options - I2C_TXE#
            /// </summary>
            public const byte FT_CBUS_I2C_TXE = 15;

            /// <summary>
            /// FT X-Series CBUS EEPROM options - I2C_RXF#
            /// </summary>
            public const byte FT_CBUS_I2C_RXF = 16;

            /// <summary>
            /// FT X-Series CBUS EEPROM options - VBUS_Sense
            /// </summary>
            public const byte FT_CBUS_VBUS_Sense = 17;

            /// <summary>
            /// FT X-Series CBUS EEPROM options - BitBang_WR#
            /// </summary>
            public const byte FT_CBUS_BitBang_WR = 18;

            /// <summary>
            /// FT X-Series CBUS EEPROM options - BitBang_RD#
            /// </summary>
            public const byte FT_CBUS_BitBang_RD = 19;

            /// <summary>
            /// FT X-Series CBUS EEPROM options - Time_Stampe
            /// </summary>
            public const byte FT_CBUS_Time_Stamp = 20;

            /// <summary>
            /// FT X-Series CBUS EEPROM options - Keep_Awake#
            /// </summary>
            public const byte FT_CBUS_Keep_Awake = 21;
        }

        /// <summary>
        /// Flags that provide information on the FTDI device state
        /// </summary>
        public class FT_FLAGS
        {
            /// <summary>
            /// Indicates that the device is open
            /// </summary>
            public const uint FT_FLAGS_OPENED = 1u;

            /// <summary>
            /// Indicates that the device is enumerated as a hi-speed USB device
            /// </summary>
            public const uint FT_FLAGS_HISPEED = 2u;
        }

        /// <summary>
        /// Valid values for drive current options on FT2232H, FT4232H and FT232H devices.
        /// </summary>
        public class FT_DRIVE_CURRENT
        {
            /// <summary>
            /// 4mA drive current
            /// </summary>
            public const byte FT_DRIVE_CURRENT_4MA = 4;

            /// <summary>
            /// 8mA drive current
            /// </summary>
            public const byte FT_DRIVE_CURRENT_8MA = 8;

            /// <summary>
            /// 12mA drive current
            /// </summary>
            public const byte FT_DRIVE_CURRENT_12MA = 12;

            /// <summary>
            /// 16mA drive current
            /// </summary>
            public const byte FT_DRIVE_CURRENT_16MA = 16;
        }

        /// <summary>
        /// List of FTDI device types
        /// </summary>
        public enum FT_DEVICE
        {
            /// <summary>
            /// FT232B or FT245B device
            /// </summary>
            FT_DEVICE_BM,
            /// <summary>
            /// FT8U232AM or FT8U245AM device
            /// </summary>
            FT_DEVICE_AM,
            /// 1
            /// <summary>
            /// FT8U100AX device
            /// </summary>
            FT_DEVICE_100AX,
            /// <summary>
            /// Unknown device
            /// </summary>
            FT_DEVICE_UNKNOWN,
            /// <summary>
            /// FT2232 device
            /// </summary>
            FT_DEVICE_2232,
            /// <summary>
            /// FT232R or FT245R device
            /// </summary>
            FT_DEVICE_232R,
            /// 5
            /// <summary>
            /// FT2232H device
            /// </summary>
            FT_DEVICE_2232H,
            /// 6
            /// <summary>
            /// FT4232H device
            /// </summary>
            FT_DEVICE_4232H,
            /// 7
            /// <summary>
            /// FT232H device
            /// </summary>
            FT_DEVICE_232H,
            /// 8
            /// <summary>
            /// FT X-Series device
            /// </summary>
            FT_DEVICE_X_SERIES,
            /// 9
            /// <summary>
            /// FT4222 hi-speed device Mode 0 - 2 interfaces
            /// </summary>
            FT_DEVICE_4222H_0,
            /// 10
            /// <summary>
            /// FT4222 hi-speed device Mode 1 or 2 - 4 interfaces
            /// </summary>
            FT_DEVICE_4222H_1_2,
            /// 11
            /// <summary>
            /// FT4222 hi-speed device Mode 3 - 1 interface
            /// </summary>
            FT_DEVICE_4222H_3,
            /// 12
            /// <summary>
            /// OTP programmer board for the FT4222.
            /// </summary>
            FT_DEVICE_4222_PROG,
            /// 13
            /// <summary>
            /// OTP programmer board for the FT900.
            /// </summary>
            FT_DEVICE_FT900,
            /// 14
            /// <summary>
            /// OTP programmer board for the FT930.
            /// </summary>
            FT_DEVICE_FT930,
            /// 15
            /// <summary>
            /// Flash programmer board for the UMFTPD3A.
            /// </summary>
            FT_DEVICE_UMFTPD3A,
            /// 16
            /// <summary>
            /// FT2233HP hi-speed device.
            /// </summary>
            FT_DEVICE_2233HP,
            /// 17
            /// <summary>
            /// FT4233HP hi-speed device.
            /// </summary>
            FT_DEVICE_4233HP,
            /// 18
            /// <summary>
            /// FT2233HP hi-speed device.
            /// </summary>
            FT_DEVICE_2232HP,
            /// 19
            /// <summary>
            /// FT4233HP hi-speed device.
            /// </summary>
            FT_DEVICE_4232HP,
            /// 20
            /// <summary>
            /// FT233HP hi-speed device.
            /// </summary>
            FT_DEVICE_233HP,
            /// 21
            /// <summary>
            /// FT232HP hi-speed device.
            /// </summary>
            FT_DEVICE_232HP,
            /// 22
            /// <summary>
            /// FT2233HA hi-speed device.
            /// </summary>
            FT_DEVICE_2232HA,
            /// 23
            /// <summary>
            /// FT4233HA hi-speed device.
            /// </summary>
            FT_DEVICE_4232HA
        }

        /// <summary>
        /// Type that holds device information for GetDeviceInformation method.
        /// Used with FT_GetDeviceInfo and FT_GetDeviceInfoDetail in FTD2XX.DLL
        /// </summary>
        public class FT_DEVICE_INFO_NODE
        {
            /// <summary>
            /// Indicates device state.  Can be any combination of the following: FT_FLAGS_OPENED, FT_FLAGS_HISPEED
            /// </summary>
            public uint Flags;

            /// <summary>
            /// Indicates the device type.  Can be one of the following: FT_DEVICE_232R, FT_DEVICE_2232C, FT_DEVICE_BM, FT_DEVICE_AM, FT_DEVICE_100AX or FT_DEVICE_UNKNOWN
            /// </summary>
            public FT_DEVICE Type;

            /// <summary>
            /// The Vendor ID and Product ID of the device
            /// </summary>
            public uint ID;

            /// <summary>
            /// The physical location identifier of the device
            /// </summary>
            public uint LocId;

            /// <summary>
            /// The device serial number
            /// </summary>
            public string SerialNumber;

            /// <summary>
            /// The device description
            /// </summary>
            public string Description;

            /// <summary>
            /// The device handle.  This value is not used externally and is provided for information only.
            /// If the device is not open, this value is 0.
            /// </summary>
            public IntPtr ftHandle;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        private class FT_PROGRAM_DATA
        {
            public uint Signature1;

            public uint Signature2;

            public uint Version;

            public ushort VendorID;

            public ushort ProductID;

            public IntPtr Manufacturer;

            public IntPtr ManufacturerID;

            public IntPtr Description;

            public IntPtr SerialNumber;

            public ushort MaxPower;

            public ushort PnP;

            public ushort SelfPowered;

            public ushort RemoteWakeup;

            public byte Rev4;

            public byte IsoIn;

            public byte IsoOut;

            public byte PullDownEnable;

            public byte SerNumEnable;

            public byte USBVersionEnable;

            public ushort USBVersion;

            public byte Rev5;

            public byte IsoInA;

            public byte IsoInB;

            public byte IsoOutA;

            public byte IsoOutB;

            public byte PullDownEnable5;

            public byte SerNumEnable5;

            public byte USBVersionEnable5;

            public ushort USBVersion5;

            public byte AIsHighCurrent;

            public byte BIsHighCurrent;

            public byte IFAIsFifo;

            public byte IFAIsFifoTar;

            public byte IFAIsFastSer;

            public byte AIsVCP;

            public byte IFBIsFifo;

            public byte IFBIsFifoTar;

            public byte IFBIsFastSer;

            public byte BIsVCP;

            public byte UseExtOsc;

            public byte HighDriveIOs;

            public byte EndpointSize;

            public byte PullDownEnableR;

            public byte SerNumEnableR;

            public byte InvertTXD;

            public byte InvertRXD;

            public byte InvertRTS;

            public byte InvertCTS;

            public byte InvertDTR;

            public byte InvertDSR;

            public byte InvertDCD;

            public byte InvertRI;

            public byte Cbus0;

            public byte Cbus1;

            public byte Cbus2;

            public byte Cbus3;

            public byte Cbus4;

            public byte RIsD2XX;

            public byte PullDownEnable7;

            public byte SerNumEnable7;

            public byte ALSlowSlew;

            public byte ALSchmittInput;

            public byte ALDriveCurrent;

            public byte AHSlowSlew;

            public byte AHSchmittInput;

            public byte AHDriveCurrent;

            public byte BLSlowSlew;

            public byte BLSchmittInput;

            public byte BLDriveCurrent;

            public byte BHSlowSlew;

            public byte BHSchmittInput;

            public byte BHDriveCurrent;

            public byte IFAIsFifo7;

            public byte IFAIsFifoTar7;

            public byte IFAIsFastSer7;

            public byte AIsVCP7;

            public byte IFBIsFifo7;

            public byte IFBIsFifoTar7;

            public byte IFBIsFastSer7;

            public byte BIsVCP7;

            public byte PowerSaveEnable;

            public byte PullDownEnable8;

            public byte SerNumEnable8;

            public byte ASlowSlew;

            public byte ASchmittInput;

            public byte ADriveCurrent;

            public byte BSlowSlew;

            public byte BSchmittInput;

            public byte BDriveCurrent;

            public byte CSlowSlew;

            public byte CSchmittInput;

            public byte CDriveCurrent;

            public byte DSlowSlew;

            public byte DSchmittInput;

            public byte DDriveCurrent;

            public byte ARIIsTXDEN;

            public byte BRIIsTXDEN;

            public byte CRIIsTXDEN;

            public byte DRIIsTXDEN;

            public byte AIsVCP8;

            public byte BIsVCP8;

            public byte CIsVCP8;

            public byte DIsVCP8;

            public byte PullDownEnableH;

            public byte SerNumEnableH;

            public byte ACSlowSlewH;

            public byte ACSchmittInputH;

            public byte ACDriveCurrentH;

            public byte ADSlowSlewH;

            public byte ADSchmittInputH;

            public byte ADDriveCurrentH;

            public byte Cbus0H;

            public byte Cbus1H;

            public byte Cbus2H;

            public byte Cbus3H;

            public byte Cbus4H;

            public byte Cbus5H;

            public byte Cbus6H;

            public byte Cbus7H;

            public byte Cbus8H;

            public byte Cbus9H;

            public byte IsFifoH;

            public byte IsFifoTarH;

            public byte IsFastSerH;

            public byte IsFT1248H;

            public byte FT1248CpolH;

            public byte FT1248LsbH;

            public byte FT1248FlowControlH;

            public byte IsVCPH;

            public byte PowerSaveEnableH;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        private struct FT_EEPROM_HEADER
        {
            public uint deviceType;

            public ushort VendorId;

            public ushort ProductId;

            public byte SerNumEnable;

            public ushort MaxPower;

            public byte SelfPowered;

            public byte RemoteWakeup;

            public byte PullDownEnable;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        private struct FT_XSERIES_DATA
        {
            public FT_EEPROM_HEADER common;

            public byte ACSlowSlew;

            public byte ACSchmittInput;

            public byte ACDriveCurrent;

            public byte ADSlowSlew;

            public byte ADSchmittInput;

            public byte ADDriveCurrent;

            public byte Cbus0;

            public byte Cbus1;

            public byte Cbus2;

            public byte Cbus3;

            public byte Cbus4;

            public byte Cbus5;

            public byte Cbus6;

            public byte InvertTXD;

            public byte InvertRXD;

            public byte InvertRTS;

            public byte InvertCTS;

            public byte InvertDTR;

            public byte InvertDSR;

            public byte InvertDCD;

            public byte InvertRI;

            public byte BCDEnable;

            public byte BCDForceCbusPWREN;

            public byte BCDDisableSleep;

            public ushort I2CSlaveAddress;

            public uint I2CDeviceId;

            public byte I2CDisableSchmitt;

            public byte FT1248Cpol;

            public byte FT1248Lsb;

            public byte FT1248FlowControl;

            public byte RS485EchoSuppress;

            public byte PowerSaveEnable;

            public byte DriverType;
        }


        /// <summary>
        /// Exceptions thrown by errors within the FTDI class.
        /// </summary>
        [Serializable]
        public class FT_EXCEPTION : Exception
        {
            /// <summary>
            ///
            /// </summary>
            public FT_EXCEPTION()
            {
            }

            /// <summary>
            ///
            /// </summary>
            /// <param name="message"></param>
            public FT_EXCEPTION(string message)
                : base(message)
            {
            }

            /// <summary>
            ///
            /// </summary>
            /// <param name="message"></param>
            /// <param name="inner"></param>
            public FT_EXCEPTION(string message, Exception inner)
                : base(message, inner)
            {
            }

            /// <summary>
            ///
            /// </summary>
            /// <param name="info"></param>
            /// <param name="context"></param>
            protected FT_EXCEPTION(SerializationInfo info, StreamingContext context)
                : base(info, context)
            {
            }
        }        
    }
}