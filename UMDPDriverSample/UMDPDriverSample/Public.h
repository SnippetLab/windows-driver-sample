/*++

Module Name:

    public.h

Abstract:

    This module contains the common declarations shared by driver
    and user applications.

Environment:

    driver and application

--*/

//
// Define an Interface Guid so that apps can find the device and talk to it.
//

DEFINE_GUID (GUID_DEVINTERFACE_UMDPDriverSample,
    0x0325c9e9,0x5c27,0x45fb,0xab,0xef,0x0d,0xad,0x75,0xc4,0x00,0x71);
// {0325c9e9-5c27-45fb-abef-0dad75c40071}
