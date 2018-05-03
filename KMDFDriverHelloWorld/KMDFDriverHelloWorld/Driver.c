#include <ntddk.h>
#include <wdf.h>

DRIVER_INITIALIZE DriverEntry;
EVT_WDF_DRIVER_DEVICE_ADD HelloWorldDeviceAdd;

NTSTATUS DriverEntry(PDRIVER_OBJECT driver, PUNICODE_STRING path) {
    NTSTATUS status;
    WDF_DRIVER_CONFIG config;
    KdPrintEx((DPFLTR_IHVDRIVER_ID, DPFLTR_INFO_LEVEL, "entry\n"));
    WDF_DRIVER_CONFIG_INIT(&config, HelloWorldDeviceAdd);
    status = WdfDriverCreate(driver, path, WDF_NO_OBJECT_ATTRIBUTES, &config, WDF_NO_HANDLE);
    return status;
}

NTSTATUS HelloWorldDeviceAdd(WDFDRIVER driver, PWDFDEVICE_INIT init) {
    NTSTATUS status;
    WDFDEVICE device;
    UNREFERENCED_PARAMETER(driver);
    status = WdfDeviceCreate(&init, WDF_NO_OBJECT_ATTRIBUTES, &device);
    return status;

}