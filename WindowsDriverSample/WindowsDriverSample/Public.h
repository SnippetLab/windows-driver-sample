/*++

Module Name:

    public.h

Abstract:

    This module contains the common declarations shared by driver
    and user applications.

Environment:

    user and kernel

--*/

//
// Define an Interface Guid so that apps can find the device and talk to it.
//

DEFINE_GUID (GUID_DEVINTERFACE_WindowsDriverSample,
    0x3825b482,0xe61e,0x44e9,0xb2,0x0b,0x79,0xf0,0xd2,0xeb,0x65,0x59);
// {3825b482-e61e-44e9-b20b-79f0d2eb6559}
