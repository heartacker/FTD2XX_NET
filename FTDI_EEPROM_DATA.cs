using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTD2XX_NET
{
    public partial class FTDI
    {
        /// <summary>
        /// Common EEPROM elements for all devices.  Inherited to specific device type EEPROMs.
        /// </summary>
        public class FT_EEPROM_DATA
        {
            /// <summary>
            /// Vendor ID as supplied by the USB Implementers Forum
            /// </summary>
            public ushort VendorID = 1027;

            /// <summary>
            /// Product ID
            /// </summary>
            public ushort ProductID = 24577;

            /// <summary>
            /// Manufacturer name string
            /// </summary>
            public string Manufacturer = "FTDI";

            /// <summary>
            /// Manufacturer name abbreviation to be used as a prefix for automatically generated serial numbers
            /// </summary>
            public string ManufacturerID = "FT";

            /// <summary>
            /// Device description string
            /// </summary>
            public string Description = "USB-Serial Converter";

            /// <summary>
            /// Device serial number string
            /// </summary>
            public string SerialNumber = "";

            /// <summary>
            /// Maximum power the device needs
            /// </summary>
            public ushort MaxPower = 144;

            /// <summary>
            /// Indicates if the device has its own power supply (self-powered) or gets power from the USB port (bus-powered)
            /// </summary>
            public bool SelfPowered;

            /// <summary>
            /// Determines if the device can wake the host PC from suspend by toggling the RI line
            /// </summary>
            public bool RemoteWakeup;
        }

        /// <summary>
        /// EEPROM structure specific to FT232B and FT245B devices.
        /// Inherits from FT_EEPROM_DATA.
        /// </summary>
        public class FT232B_EEPROM_STRUCTURE : FT_EEPROM_DATA
        {
            /// <summary>
            /// Determines if IOs are pulled down when the device is in suspend
            /// </summary>
            public bool PullDownEnable;

            /// <summary>
            /// Determines if the serial number is enabled
            /// </summary>
            public bool SerNumEnable = true;

            /// <summary>
            /// Determines if the USB version number is enabled
            /// </summary>
            public bool USBVersionEnable = true;

            /// <summary>
            /// The USB version number.  Should be either 0x0110 (USB 1.1) or 0x0200 (USB 2.0)
            /// </summary>
            public ushort USBVersion = 512;
        }

        /// <summary>
        /// EEPROM structure specific to FT2232 devices.
        /// Inherits from FT_EEPROM_DATA.
        /// </summary>
        public class FT2232_EEPROM_STRUCTURE : FT_EEPROM_DATA
        {
            /// <summary>
            /// Determines if IOs are pulled down when the device is in suspend
            /// </summary>
            public bool PullDownEnable;

            /// <summary>
            /// Determines if the serial number is enabled
            /// </summary>
            public bool SerNumEnable = true;

            /// <summary>
            /// Determines if the USB version number is enabled
            /// </summary>
            public bool USBVersionEnable = true;

            /// <summary>
            /// The USB version number.  Should be either 0x0110 (USB 1.1) or 0x0200 (USB 2.0)
            /// </summary>
            public ushort USBVersion = 512;

            /// <summary>
            /// Enables high current IOs on channel A
            /// </summary>
            public bool AIsHighCurrent;

            /// <summary>
            /// Enables high current IOs on channel B
            /// </summary>
            public bool BIsHighCurrent;

            /// <summary>
            /// Determines if channel A is in FIFO mode
            /// </summary>
            public bool IFAIsFifo;

            /// <summary>
            /// Determines if channel A is in FIFO target mode
            /// </summary>
            public bool IFAIsFifoTar;

            /// <summary>
            /// Determines if channel A is in fast serial mode
            /// </summary>
            public bool IFAIsFastSer;

            /// <summary>
            /// Determines if channel A loads the VCP driver
            /// </summary>
            public bool AIsVCP = true;

            /// <summary>
            /// Determines if channel B is in FIFO mode
            /// </summary>
            public bool IFBIsFifo;

            /// <summary>
            /// Determines if channel B is in FIFO target mode
            /// </summary>
            public bool IFBIsFifoTar;

            /// <summary>
            /// Determines if channel B is in fast serial mode
            /// </summary>
            public bool IFBIsFastSer;

            /// <summary>
            /// Determines if channel B loads the VCP driver
            /// </summary>
            public bool BIsVCP = true;
        }

        /// <summary>
        /// EEPROM structure specific to FT232R and FT245R devices.
        /// Inherits from FT_EEPROM_DATA.
        /// </summary>
        public class FT232R_EEPROM_STRUCTURE : FT_EEPROM_DATA
        {
            /// <summary>
            /// Disables the FT232R internal clock source.
            /// If the device has external oscillator enabled it must have an external oscillator fitted to function
            /// </summary>
            public bool UseExtOsc;

            /// <summary>
            /// Enables high current IOs
            /// </summary>
            public bool HighDriveIOs;

            /// <summary>
            /// Sets the endpoint size.  This should always be set to 64
            /// </summary>
            public byte EndpointSize = 64;

            /// <summary>
            /// Determines if IOs are pulled down when the device is in suspend
            /// </summary>
            public bool PullDownEnable;

            /// <summary>
            /// Determines if the serial number is enabled
            /// </summary>
            public bool SerNumEnable = true;

            /// <summary>
            /// Inverts the sense of the TXD line
            /// </summary>
            public bool InvertTXD;

            /// <summary>
            /// Inverts the sense of the RXD line
            /// </summary>
            public bool InvertRXD;

            /// <summary>
            /// Inverts the sense of the RTS line
            /// </summary>
            public bool InvertRTS;

            /// <summary>
            /// Inverts the sense of the CTS line
            /// </summary>
            public bool InvertCTS;

            /// <summary>
            /// Inverts the sense of the DTR line
            /// </summary>
            public bool InvertDTR;

            /// <summary>
            /// Inverts the sense of the DSR line
            /// </summary>
            public bool InvertDSR;

            /// <summary>
            /// Inverts the sense of the DCD line
            /// </summary>
            public bool InvertDCD;

            /// <summary>
            /// Inverts the sense of the RI line
            /// </summary>
            public bool InvertRI;

            /// <summary>
            /// Sets the function of the CBUS0 pin for FT232R devices.
            /// Valid values are FT_CBUS_TXDEN, FT_CBUS_PWRON , FT_CBUS_RXLED, FT_CBUS_TXLED,
            /// FT_CBUS_TXRXLED, FT_CBUS_SLEEP, FT_CBUS_CLK48, FT_CBUS_CLK24, FT_CBUS_CLK12,
            /// FT_CBUS_CLK6, FT_CBUS_IOMODE, FT_CBUS_BITBANG_WR, FT_CBUS_BITBANG_RD
            /// </summary>
            public byte Cbus0 = 5;

            /// <summary>
            /// Sets the function of the CBUS1 pin for FT232R devices.
            /// Valid values are FT_CBUS_TXDEN, FT_CBUS_PWRON , FT_CBUS_RXLED, FT_CBUS_TXLED,
            /// FT_CBUS_TXRXLED, FT_CBUS_SLEEP, FT_CBUS_CLK48, FT_CBUS_CLK24, FT_CBUS_CLK12,
            /// FT_CBUS_CLK6, FT_CBUS_IOMODE, FT_CBUS_BITBANG_WR, FT_CBUS_BITBANG_RD
            /// </summary>
            public byte Cbus1 = 5;

            /// <summary>
            /// Sets the function of the CBUS2 pin for FT232R devices.
            /// Valid values are FT_CBUS_TXDEN, FT_CBUS_PWRON , FT_CBUS_RXLED, FT_CBUS_TXLED,
            /// FT_CBUS_TXRXLED, FT_CBUS_SLEEP, FT_CBUS_CLK48, FT_CBUS_CLK24, FT_CBUS_CLK12,
            /// FT_CBUS_CLK6, FT_CBUS_IOMODE, FT_CBUS_BITBANG_WR, FT_CBUS_BITBANG_RD
            /// </summary>
            public byte Cbus2 = 5;

            /// <summary>
            /// Sets the function of the CBUS3 pin for FT232R devices.
            /// Valid values are FT_CBUS_TXDEN, FT_CBUS_PWRON , FT_CBUS_RXLED, FT_CBUS_TXLED,
            /// FT_CBUS_TXRXLED, FT_CBUS_SLEEP, FT_CBUS_CLK48, FT_CBUS_CLK24, FT_CBUS_CLK12,
            /// FT_CBUS_CLK6, FT_CBUS_IOMODE, FT_CBUS_BITBANG_WR, FT_CBUS_BITBANG_RD
            /// </summary>
            public byte Cbus3 = 5;

            /// <summary>
            /// Sets the function of the CBUS4 pin for FT232R devices.
            /// Valid values are FT_CBUS_TXDEN, FT_CBUS_PWRON , FT_CBUS_RXLED, FT_CBUS_TXLED,
            /// FT_CBUS_TXRXLED, FT_CBUS_SLEEP, FT_CBUS_CLK48, FT_CBUS_CLK24, FT_CBUS_CLK12,
            /// FT_CBUS_CLK6
            /// </summary>
            public byte Cbus4 = 5;

            /// <summary>
            /// Determines if the VCP driver is loaded
            /// </summary>
            public bool RIsD2XX;
        }

        /// <summary>
        /// EEPROM structure specific to FT2232H devices.
        /// Inherits from FT_EEPROM_DATA.
        /// </summary>
        public class FT2232H_EEPROM_STRUCTURE : FT_EEPROM_DATA
        {
            /// <summary>
            /// Determines if IOs are pulled down when the device is in suspend
            /// </summary>
            public bool PullDownEnable;

            /// <summary>
            /// Determines if the serial number is enabled
            /// </summary>
            public bool SerNumEnable = true;

            /// <summary>
            /// Determines if AL pins have a slow slew rate
            /// </summary>
            public bool ALSlowSlew;

            /// <summary>
            /// Determines if the AL pins have a Schmitt input
            /// </summary>
            public bool ALSchmittInput;

            /// <summary>
            /// Determines the AL pins drive current in mA.  Valid values are FT_DRIVE_CURRENT_4MA, FT_DRIVE_CURRENT_8MA, FT_DRIVE_CURRENT_12MA or FT_DRIVE_CURRENT_16MA
            /// </summary>
            public byte ALDriveCurrent = 4;

            /// <summary>
            /// Determines if AH pins have a slow slew rate
            /// </summary>
            public bool AHSlowSlew;

            /// <summary>
            /// Determines if the AH pins have a Schmitt input
            /// </summary>
            public bool AHSchmittInput;

            /// <summary>
            /// Determines the AH pins drive current in mA.  Valid values are FT_DRIVE_CURRENT_4MA, FT_DRIVE_CURRENT_8MA, FT_DRIVE_CURRENT_12MA or FT_DRIVE_CURRENT_16MA
            /// </summary>
            public byte AHDriveCurrent = 4;

            /// <summary>
            /// Determines if BL pins have a slow slew rate
            /// </summary>
            public bool BLSlowSlew;

            /// <summary>
            /// Determines if the BL pins have a Schmitt input
            /// </summary>
            public bool BLSchmittInput;

            /// <summary>
            /// Determines the BL pins drive current in mA.  Valid values are FT_DRIVE_CURRENT_4MA, FT_DRIVE_CURRENT_8MA, FT_DRIVE_CURRENT_12MA or FT_DRIVE_CURRENT_16MA
            /// </summary>
            public byte BLDriveCurrent = 4;

            /// <summary>
            /// Determines if BH pins have a slow slew rate
            /// </summary>
            public bool BHSlowSlew;

            /// <summary>
            /// Determines if the BH pins have a Schmitt input
            /// </summary>
            public bool BHSchmittInput;

            /// <summary>
            /// Determines the BH pins drive current in mA.  Valid values are FT_DRIVE_CURRENT_4MA, FT_DRIVE_CURRENT_8MA, FT_DRIVE_CURRENT_12MA or FT_DRIVE_CURRENT_16MA
            /// </summary>
            public byte BHDriveCurrent = 4;

            /// <summary>
            /// Determines if channel A is in FIFO mode
            /// </summary>
            public bool IFAIsFifo;

            /// <summary>
            /// Determines if channel A is in FIFO target mode
            /// </summary>
            public bool IFAIsFifoTar;

            /// <summary>
            /// Determines if channel A is in fast serial mode
            /// </summary>
            public bool IFAIsFastSer;

            /// <summary>
            /// Determines if channel A loads the VCP driver
            /// </summary>
            public bool AIsVCP = true;

            /// <summary>
            /// Determines if channel B is in FIFO mode
            /// </summary>
            public bool IFBIsFifo;

            /// <summary>
            /// Determines if channel B is in FIFO target mode
            /// </summary>
            public bool IFBIsFifoTar;

            /// <summary>
            /// Determines if channel B is in fast serial mode
            /// </summary>
            public bool IFBIsFastSer;

            /// <summary>
            /// Determines if channel B loads the VCP driver
            /// </summary>
            public bool BIsVCP = true;

            /// <summary>
            /// For self-powered designs, keeps the FT2232H in low power state until BCBUS7 is high
            /// </summary>
            public bool PowerSaveEnable;
        }

        /// <summary>
        /// EEPROM structure specific to FT4232H devices.
        /// Inherits from FT_EEPROM_DATA.
        /// </summary>
        public class FT4232H_EEPROM_STRUCTURE : FT_EEPROM_DATA
        {
            /// <summary>
            /// Determines if IOs are pulled down when the device is in suspend
            /// </summary>
            public bool PullDownEnable;

            /// <summary>
            /// Determines if the serial number is enabled
            /// </summary>
            public bool SerNumEnable = true;

            /// <summary>
            /// Determines if A pins have a slow slew rate
            /// </summary>
            public bool ASlowSlew;

            /// <summary>
            /// Determines if the A pins have a Schmitt input
            /// </summary>
            public bool ASchmittInput;

            /// <summary>
            /// Determines the A pins drive current in mA.  Valid values are FT_DRIVE_CURRENT_4MA, FT_DRIVE_CURRENT_8MA, FT_DRIVE_CURRENT_12MA or FT_DRIVE_CURRENT_16MA
            /// </summary>
            public byte ADriveCurrent = 4;

            /// <summary>
            /// Determines if B pins have a slow slew rate
            /// </summary>
            public bool BSlowSlew;

            /// <summary>
            /// Determines if the B pins have a Schmitt input
            /// </summary>
            public bool BSchmittInput;

            /// <summary>
            /// Determines the B pins drive current in mA.  Valid values are FT_DRIVE_CURRENT_4MA, FT_DRIVE_CURRENT_8MA, FT_DRIVE_CURRENT_12MA or FT_DRIVE_CURRENT_16MA
            /// </summary>
            public byte BDriveCurrent = 4;

            /// <summary>
            /// Determines if C pins have a slow slew rate
            /// </summary>
            public bool CSlowSlew;

            /// <summary>
            /// Determines if the C pins have a Schmitt input
            /// </summary>
            public bool CSchmittInput;

            /// <summary>
            /// Determines the C pins drive current in mA.  Valid values are FT_DRIVE_CURRENT_4MA, FT_DRIVE_CURRENT_8MA, FT_DRIVE_CURRENT_12MA or FT_DRIVE_CURRENT_16MA
            /// </summary>
            public byte CDriveCurrent = 4;

            /// <summary>
            /// Determines if D pins have a slow slew rate
            /// </summary>
            public bool DSlowSlew;

            /// <summary>
            /// Determines if the D pins have a Schmitt input
            /// </summary>
            public bool DSchmittInput;

            /// <summary>
            /// Determines the D pins drive current in mA.  Valid values are FT_DRIVE_CURRENT_4MA, FT_DRIVE_CURRENT_8MA, FT_DRIVE_CURRENT_12MA or FT_DRIVE_CURRENT_16MA
            /// </summary>
            public byte DDriveCurrent = 4;

            /// <summary>
            /// RI of port A acts as RS485 transmit enable (TXDEN)
            /// </summary>
            public bool ARIIsTXDEN;

            /// <summary>
            /// RI of port B acts as RS485 transmit enable (TXDEN)
            /// </summary>
            public bool BRIIsTXDEN;

            /// <summary>
            /// RI of port C acts as RS485 transmit enable (TXDEN)
            /// </summary>
            public bool CRIIsTXDEN;

            /// <summary>
            /// RI of port D acts as RS485 transmit enable (TXDEN)
            /// </summary>
            public bool DRIIsTXDEN;

            /// <summary>
            /// Determines if channel A loads the VCP driver
            /// </summary>
            public bool AIsVCP = true;

            /// <summary>
            /// Determines if channel B loads the VCP driver
            /// </summary>
            public bool BIsVCP = true;

            /// <summary>
            /// Determines if channel C loads the VCP driver
            /// </summary>
            public bool CIsVCP = true;

            /// <summary>
            /// Determines if channel D loads the VCP driver
            /// </summary>
            public bool DIsVCP = true;
        }

        /// <summary>
        /// EEPROM structure specific to FT232H devices.
        /// Inherits from FT_EEPROM_DATA.
        /// </summary>
        public class FT232H_EEPROM_STRUCTURE : FT_EEPROM_DATA
        {
            /// <summary>
            /// Determines if IOs are pulled down when the device is in suspend
            /// </summary>
            public bool PullDownEnable;

            /// <summary>
            /// Determines if the serial number is enabled
            /// </summary>
            public bool SerNumEnable = true;

            /// <summary>
            /// Determines if AC pins have a slow slew rate
            /// </summary>
            public bool ACSlowSlew;

            /// <summary>
            /// Determines if the AC pins have a Schmitt input
            /// </summary>
            public bool ACSchmittInput;

            /// <summary>
            /// Determines the AC pins drive current in mA.  Valid values are FT_DRIVE_CURRENT_4MA, FT_DRIVE_CURRENT_8MA, FT_DRIVE_CURRENT_12MA or FT_DRIVE_CURRENT_16MA
            /// </summary>
            public byte ACDriveCurrent = 4;

            /// <summary>
            /// Determines if AD pins have a slow slew rate
            /// </summary>
            public bool ADSlowSlew;

            /// <summary>
            /// Determines if the AD pins have a Schmitt input
            /// </summary>
            public bool ADSchmittInput;

            /// <summary>
            /// Determines the AD pins drive current in mA.  Valid values are FT_DRIVE_CURRENT_4MA, FT_DRIVE_CURRENT_8MA, FT_DRIVE_CURRENT_12MA or FT_DRIVE_CURRENT_16MA
            /// </summary>
            public byte ADDriveCurrent = 4;

            /// <summary>
            /// Sets the function of the CBUS0 pin for FT232H devices.
            /// Valid values are FT_CBUS_TRISTATE, FT_CBUS_RXLED, FT_CBUS_TXLED, FT_CBUS_TXRXLED,
            /// FT_CBUS_PWREN, FT_CBUS_SLEEP, FT_CBUS_DRIVE_0, FT_CBUS_DRIVE_1, FT_CBUS_TXDEN, FT_CBUS_CLK30,
            /// FT_CBUS_CLK15, FT_CBUS_CLK7_5
            /// </summary>
            public byte Cbus0;

            /// <summary>
            /// Sets the function of the CBUS1 pin for FT232H devices.
            /// Valid values are FT_CBUS_TRISTATE, FT_CBUS_RXLED, FT_CBUS_TXLED, FT_CBUS_TXRXLED,
            /// FT_CBUS_PWREN, FT_CBUS_SLEEP, FT_CBUS_DRIVE_0, FT_CBUS_DRIVE_1, FT_CBUS_TXDEN, FT_CBUS_CLK30,
            /// FT_CBUS_CLK15, FT_CBUS_CLK7_5
            /// </summary>
            public byte Cbus1;

            /// <summary>
            /// Sets the function of the CBUS2 pin for FT232H devices.
            /// Valid values are FT_CBUS_TRISTATE, FT_CBUS_RXLED, FT_CBUS_TXLED, FT_CBUS_TXRXLED,
            /// FT_CBUS_PWREN, FT_CBUS_SLEEP, FT_CBUS_DRIVE_0, FT_CBUS_DRIVE_1, FT_CBUS_TXDEN
            /// </summary>
            public byte Cbus2;

            /// <summary>
            /// Sets the function of the CBUS3 pin for FT232H devices.
            /// Valid values are FT_CBUS_TRISTATE, FT_CBUS_RXLED, FT_CBUS_TXLED, FT_CBUS_TXRXLED,
            /// FT_CBUS_PWREN, FT_CBUS_SLEEP, FT_CBUS_DRIVE_0, FT_CBUS_DRIVE_1, FT_CBUS_TXDEN
            /// </summary>
            public byte Cbus3;

            /// <summary>
            /// Sets the function of the CBUS4 pin for FT232H devices.
            /// Valid values are FT_CBUS_TRISTATE, FT_CBUS_RXLED, FT_CBUS_TXLED, FT_CBUS_TXRXLED,
            /// FT_CBUS_PWREN, FT_CBUS_SLEEP, FT_CBUS_DRIVE_0, FT_CBUS_DRIVE_1, FT_CBUS_TXDEN
            /// </summary>
            public byte Cbus4;

            /// <summary>
            /// Sets the function of the CBUS5 pin for FT232H devices.
            /// Valid values are FT_CBUS_TRISTATE, FT_CBUS_RXLED, FT_CBUS_TXLED, FT_CBUS_TXRXLED,
            /// FT_CBUS_PWREN, FT_CBUS_SLEEP, FT_CBUS_DRIVE_0, FT_CBUS_DRIVE_1, FT_CBUS_IOMODE,
            /// FT_CBUS_TXDEN, FT_CBUS_CLK30, FT_CBUS_CLK15, FT_CBUS_CLK7_5
            /// </summary>
            public byte Cbus5;

            /// <summary>
            /// Sets the function of the CBUS6 pin for FT232H devices.
            /// Valid values are FT_CBUS_TRISTATE, FT_CBUS_RXLED, FT_CBUS_TXLED, FT_CBUS_TXRXLED,
            /// FT_CBUS_PWREN, FT_CBUS_SLEEP, FT_CBUS_DRIVE_0, FT_CBUS_DRIVE_1, FT_CBUS_IOMODE,
            /// FT_CBUS_TXDEN, FT_CBUS_CLK30, FT_CBUS_CLK15, FT_CBUS_CLK7_5
            /// </summary>
            public byte Cbus6;

            /// <summary>
            /// Sets the function of the CBUS7 pin for FT232H devices.
            /// Valid values are FT_CBUS_TRISTATE
            /// </summary>
            public byte Cbus7;

            /// <summary>
            /// Sets the function of the CBUS8 pin for FT232H devices.
            /// Valid values are FT_CBUS_TRISTATE, FT_CBUS_RXLED, FT_CBUS_TXLED, FT_CBUS_TXRXLED,
            /// FT_CBUS_PWREN, FT_CBUS_SLEEP, FT_CBUS_DRIVE_0, FT_CBUS_DRIVE_1, FT_CBUS_IOMODE,
            /// FT_CBUS_TXDEN, FT_CBUS_CLK30, FT_CBUS_CLK15, FT_CBUS_CLK7_5
            /// </summary>
            public byte Cbus8;

            /// <summary>
            /// Sets the function of the CBUS9 pin for FT232H devices.
            /// Valid values are FT_CBUS_TRISTATE, FT_CBUS_RXLED, FT_CBUS_TXLED, FT_CBUS_TXRXLED,
            /// FT_CBUS_PWREN, FT_CBUS_SLEEP, FT_CBUS_DRIVE_0, FT_CBUS_DRIVE_1, FT_CBUS_IOMODE,
            /// FT_CBUS_TXDEN, FT_CBUS_CLK30, FT_CBUS_CLK15, FT_CBUS_CLK7_5
            /// </summary>
            public byte Cbus9;

            /// <summary>
            /// Determines if the device is in FIFO mode
            /// </summary>
            public bool IsFifo;

            /// <summary>
            /// Determines if the device is in FIFO target mode
            /// </summary>
            public bool IsFifoTar;

            /// <summary>
            /// Determines if the device is in fast serial mode
            /// </summary>
            public bool IsFastSer;

            /// <summary>
            /// Determines if the device is in FT1248 mode
            /// </summary>
            public bool IsFT1248;

            /// <summary>
            /// Determines FT1248 mode clock polarity
            /// </summary>
            public bool FT1248Cpol;

            /// <summary>
            /// Determines if data is ent MSB (0) or LSB (1) in FT1248 mode
            /// </summary>
            public bool FT1248Lsb;

            /// <summary>
            /// Determines if FT1248 mode uses flow control
            /// </summary>
            public bool FT1248FlowControl;

            /// <summary>
            /// Determines if the VCP driver is loaded
            /// </summary>
            public bool IsVCP = true;

            /// <summary>
            /// For self-powered designs, keeps the FT232H in low power state until ACBUS7 is high
            /// </summary>
            public bool PowerSaveEnable;
        }

        /// <summary>
        /// EEPROM structure specific to X-Series devices.
        /// Inherits from FT_EEPROM_DATA.
        /// </summary>
        public class FT_XSERIES_EEPROM_STRUCTURE : FT_EEPROM_DATA
        {
            /// <summary>
            /// Determines if IOs are pulled down when the device is in suspend
            /// </summary>
            public bool PullDownEnable;

            /// <summary>
            /// Determines if the serial number is enabled
            /// </summary>
            public bool SerNumEnable = true;

            /// <summary>
            /// Determines if the USB version number is enabled
            /// </summary>
            public bool USBVersionEnable = true;

            /// <summary>
            /// The USB version number: 0x0200 (USB 2.0)
            /// </summary>
            public ushort USBVersion = 512;

            /// <summary>
            /// Determines if AC pins have a slow slew rate
            /// </summary>
            public byte ACSlowSlew;

            /// <summary>
            /// Determines if the AC pins have a Schmitt input
            /// </summary>
            public byte ACSchmittInput;

            /// <summary>
            /// Determines the AC pins drive current in mA.  Valid values are FT_DRIVE_CURRENT_4MA, FT_DRIVE_CURRENT_8MA, FT_DRIVE_CURRENT_12MA or FT_DRIVE_CURRENT_16MA
            /// </summary>
            public byte ACDriveCurrent;

            /// <summary>
            /// Determines if AD pins have a slow slew rate
            /// </summary>
            public byte ADSlowSlew;

            /// <summary>
            /// Determines if AD pins have a schmitt input
            /// </summary>
            public byte ADSchmittInput;

            /// <summary>
            /// Determines the AD pins drive current in mA.  Valid values are FT_DRIVE_CURRENT_4MA, FT_DRIVE_CURRENT_8MA, FT_DRIVE_CURRENT_12MA or FT_DRIVE_CURRENT_16MA
            /// </summary>
            public byte ADDriveCurrent;

            /// <summary>
            /// Sets the function of the CBUS0 pin for FT232H devices.
            /// Valid values are FT_CBUS_TRISTATE, FT_CBUS_RXLED, FT_CBUS_TXLED, FT_CBUS_TXRXLED,
            /// FT_CBUS_PWREN, FT_CBUS_SLEEP, FT_CBUS_DRIVE_0, FT_CBUS_DRIVE_1, FT_CBUS_GPIO, FT_CBUS_TXDEN, FT_CBUS_CLK24,
            /// FT_CBUS_CLK12, FT_CBUS_CLK6, FT_CBUS_BCD_CHARGER, FT_CBUS_BCD_CHARGER_N, FT_CBUS_VBUS_SENSE, FT_CBUS_BITBANG_WR,
            /// FT_CBUS_BITBANG_RD, FT_CBUS_TIME_STAMP, FT_CBUS_KEEP_AWAKE
            /// </summary>
            public byte Cbus0;

            /// <summary>
            /// Sets the function of the CBUS1 pin for FT232H devices.
            /// Valid values are FT_CBUS_TRISTATE, FT_CBUS_RXLED, FT_CBUS_TXLED, FT_CBUS_TXRXLED,
            /// FT_CBUS_PWREN, FT_CBUS_SLEEP, FT_CBUS_DRIVE_0, FT_CBUS_DRIVE_1, FT_CBUS_GPIO, FT_CBUS_TXDEN, FT_CBUS_CLK24,
            /// FT_CBUS_CLK12, FT_CBUS_CLK6, FT_CBUS_BCD_CHARGER, FT_CBUS_BCD_CHARGER_N, FT_CBUS_VBUS_SENSE, FT_CBUS_BITBANG_WR,
            /// FT_CBUS_BITBANG_RD, FT_CBUS_TIME_STAMP, FT_CBUS_KEEP_AWAKE
            /// </summary>
            public byte Cbus1;

            /// <summary>
            /// Sets the function of the CBUS2 pin for FT232H devices.
            /// Valid values are FT_CBUS_TRISTATE, FT_CBUS_RXLED, FT_CBUS_TXLED, FT_CBUS_TXRXLED,
            /// FT_CBUS_PWREN, FT_CBUS_SLEEP, FT_CBUS_DRIVE_0, FT_CBUS_DRIVE_1, FT_CBUS_GPIO, FT_CBUS_TXDEN, FT_CBUS_CLK24,
            /// FT_CBUS_CLK12, FT_CBUS_CLK6, FT_CBUS_BCD_CHARGER, FT_CBUS_BCD_CHARGER_N, FT_CBUS_VBUS_SENSE, FT_CBUS_BITBANG_WR,
            /// FT_CBUS_BITBANG_RD, FT_CBUS_TIME_STAMP, FT_CBUS_KEEP_AWAKE
            /// </summary>
            public byte Cbus2;

            /// <summary>
            /// Sets the function of the CBUS3 pin for FT232H devices.
            /// Valid values are FT_CBUS_TRISTATE, FT_CBUS_RXLED, FT_CBUS_TXLED, FT_CBUS_TXRXLED,
            /// FT_CBUS_PWREN, FT_CBUS_SLEEP, FT_CBUS_DRIVE_0, FT_CBUS_DRIVE_1, FT_CBUS_GPIO, FT_CBUS_TXDEN, FT_CBUS_CLK24,
            /// FT_CBUS_CLK12, FT_CBUS_CLK6, FT_CBUS_BCD_CHARGER, FT_CBUS_BCD_CHARGER_N, FT_CBUS_VBUS_SENSE, FT_CBUS_BITBANG_WR,
            /// FT_CBUS_BITBANG_RD, FT_CBUS_TIME_STAMP, FT_CBUS_KEEP_AWAKE
            /// </summary>
            public byte Cbus3;

            /// <summary>
            /// Sets the function of the CBUS4 pin for FT232H devices.
            /// Valid values are FT_CBUS_TRISTATE, FT_CBUS_RXLED, FT_CBUS_TXLED, FT_CBUS_TXRXLED,
            /// FT_CBUS_PWREN, FT_CBUS_SLEEP, FT_CBUS_DRIVE_0, FT_CBUS_DRIVE_1, FT_CBUS_TXDEN, FT_CBUS_CLK24,
            /// FT_CBUS_CLK12, FT_CBUS_CLK6, FT_CBUS_BCD_CHARGER, FT_CBUS_BCD_CHARGER_N, FT_CBUS_VBUS_SENSE, FT_CBUS_BITBANG_WR,
            /// FT_CBUS_BITBANG_RD, FT_CBUS_TIME_STAMP, FT_CBUS_KEEP_AWAKE
            /// </summary>
            public byte Cbus4;

            /// <summary>
            /// Sets the function of the CBUS5 pin for FT232H devices.
            /// Valid values are FT_CBUS_TRISTATE, FT_CBUS_RXLED, FT_CBUS_TXLED, FT_CBUS_TXRXLED,
            /// FT_CBUS_PWREN, FT_CBUS_SLEEP, FT_CBUS_DRIVE_0, FT_CBUS_DRIVE_1, FT_CBUS_TXDEN, FT_CBUS_CLK24,
            /// FT_CBUS_CLK12, FT_CBUS_CLK6, FT_CBUS_BCD_CHARGER, FT_CBUS_BCD_CHARGER_N, FT_CBUS_VBUS_SENSE, FT_CBUS_BITBANG_WR,
            /// FT_CBUS_BITBANG_RD, FT_CBUS_TIME_STAMP, FT_CBUS_KEEP_AWAKE
            /// </summary>
            public byte Cbus5;

            /// <summary>
            /// Sets the function of the CBUS6 pin for FT232H devices.
            /// Valid values are FT_CBUS_TRISTATE, FT_CBUS_RXLED, FT_CBUS_TXLED, FT_CBUS_TXRXLED,
            /// FT_CBUS_PWREN, FT_CBUS_SLEEP, FT_CBUS_DRIVE_0, FT_CBUS_DRIVE_1, FT_CBUS_TXDEN, FT_CBUS_CLK24,
            /// FT_CBUS_CLK12, FT_CBUS_CLK6, FT_CBUS_BCD_CHARGER, FT_CBUS_BCD_CHARGER_N, FT_CBUS_VBUS_SENSE, FT_CBUS_BITBANG_WR,
            /// FT_CBUS_BITBANG_RD, FT_CBUS_TIME_STAMP, FT_CBUS_KEEP_AWAKE
            /// </summary>
            public byte Cbus6;

            /// <summary>
            /// Inverts the sense of the TXD line
            /// </summary>
            public byte InvertTXD;

            /// <summary>
            /// Inverts the sense of the RXD line
            /// </summary>
            public byte InvertRXD;

            /// <summary>
            /// Inverts the sense of the RTS line
            /// </summary>
            public byte InvertRTS;

            /// <summary>
            /// Inverts the sense of the CTS line
            /// </summary>
            public byte InvertCTS;

            /// <summary>
            /// Inverts the sense of the DTR line
            /// </summary>
            public byte InvertDTR;

            /// <summary>
            /// Inverts the sense of the DSR line
            /// </summary>
            public byte InvertDSR;

            /// <summary>
            /// Inverts the sense of the DCD line
            /// </summary>
            public byte InvertDCD;

            /// <summary>
            /// Inverts the sense of the RI line
            /// </summary>
            public byte InvertRI;

            /// <summary>
            /// Determines whether the Battery Charge Detection option is enabled.
            /// </summary>
            public byte BCDEnable;

            /// <summary>
            /// Asserts the power enable signal on CBUS when charging port detected.
            /// </summary>
            public byte BCDForceCbusPWREN;

            /// <summary>
            /// Forces the device never to go into sleep mode.
            /// </summary>
            public byte BCDDisableSleep;

            /// <summary>
            /// I2C slave device address.
            /// </summary>
            public ushort I2CSlaveAddress;

            /// <summary>
            /// I2C device ID
            /// </summary>
            public uint I2CDeviceId;

            /// <summary>
            /// Disable I2C Schmitt trigger.
            /// </summary>
            public byte I2CDisableSchmitt;

            /// <summary>
            /// FT1248 clock polarity - clock idle high (1) or clock idle low (0)
            /// </summary>
            public byte FT1248Cpol;

            /// <summary>
            /// FT1248 data is LSB (1) or MSB (0)
            /// </summary>
            public byte FT1248Lsb;

            /// <summary>
            /// FT1248 flow control enable.
            /// </summary>
            public byte FT1248FlowControl;

            /// <summary>
            /// Enable RS485 Echo Suppression
            /// </summary>
            public byte RS485EchoSuppress;

            /// <summary>
            /// Enable Power Save mode.
            /// </summary>
            public byte PowerSaveEnable;

            /// <summary>
            /// Determines whether the VCP driver is loaded.
            /// </summary>
            public byte IsVCP;
        }

    }
}
