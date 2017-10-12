#include "pch.h"
#include <cvt/wstring>
#include <codecvt>
using namespace devtodev::background;
using namespace com::devtodev::extension::metro;
using namespace std;

typedef void (*DTD_onRegisteredForPushNotifications)(const char*);
typedef void (*DTD_onFailedToRegisteredForPushNotifications)(const char*);
typedef void (*DTD_onPushNotificationsReceived)(const char*);
typedef void (*DTD_onPushNotificationOpened)(const char*);
DTD_onRegisteredForPushNotifications dtd_onRegisteredForPushNotifications;
DTD_onFailedToRegisteredForPushNotifications dtd_onFailedToRegisteredForPushNotifications;
DTD_onPushNotificationsReceived dtd_onPushNotificationsReceived;
DTD_onPushNotificationOpened dtd_onPushNotificationOpened;

const char* dtd_cCopy(const char* string) {
	if (string == nullptr)
		return nullptr;
	char* res = (char*)malloc(strlen(string) + 1);
	strcpy_s(res, strlen(string) + 1, string);
	return res;
}

const char* dtd_copyPlatformString(Platform::String^ string) {
	if (string == nullptr)
		return nullptr;
	stdext::cvt::wstring_convert<std::codecvt_utf8<wchar_t>> convert;
	std::string stringUtf = convert.to_bytes(string->Data());
	char* res = (char*)malloc(strlen(stringUtf.c_str()) + 1);
	strcpy_s(res, strlen(stringUtf.c_str()) + 1, stringUtf.c_str());
	return res;
}

com::devtodev::extension::metro::DeviceUtils^ dtd_deviceUtils = ref new com::devtodev::extension::metro::DeviceUtils();
com::devtodev::extension::metro::ApplicationUtils^ dtd_applicationUtils = ref new com::devtodev::extension::metro::ApplicationUtils();
com::devtodev::extension::metro::Push::PushClientMetro^ dtd_pushClient = ref new com::devtodev::extension::metro::Push::PushClientMetro();

void OnPushTokenReceived(Platform::String ^token) {
	if (dtd_onRegisteredForPushNotifications) {
		dtd_onRegisteredForPushNotifications(dtd_copyPlatformString(token));
	}
}

void OnPushTokenFailed(Platform::String ^error) {
	if (dtd_onFailedToRegisteredForPushNotifications) {
		dtd_onFailedToRegisteredForPushNotifications(dtd_copyPlatformString(error));
	}
}

void OnOnPushReceived(Platform::String ^pushJsonData) {
	if (dtd_onPushNotificationsReceived) {
		dtd_onPushNotificationsReceived(dtd_copyPlatformString(pushJsonData));
	}
}

void OnPushOpened(Platform::String ^pushJsonData) {
	if (dtd_onPushNotificationOpened) {
		dtd_onPushNotificationOpened(dtd_copyPlatformString(pushJsonData));
	}
}


extern "C" {

	int dtd_devIsRoot() {
		return dtd_deviceUtils->IsRooted();
	}

	const char * dtd_devUAgent() {
		return 	dtd_copyPlatformString(dtd_deviceUtils->GetUserAgentSring());
	}

	const char * dtd_devAdId() {
		return dtd_copyPlatformString(dtd_deviceUtils->GetAdvertismentId());
	}

	const char * dtd_devHWToken() {
		return dtd_copyPlatformString(dtd_deviceUtils->GetHardwareToken());
	}

	const char * dtd_devMobileOperator() {
		return dtd_copyPlatformString(dtd_deviceUtils->GetMobileOperator());
	}

	const char * dtd_devManufacturer() {
		return dtd_copyPlatformString(dtd_deviceUtils->GetDeviceManufacturer());
	}

	const char * dtd_devName() {
		return dtd_copyPlatformString(dtd_deviceUtils->GetDeviceOSName());
	}

	const char * dtd_devOSVer() {
		return dtd_copyPlatformString(dtd_deviceUtils->GetDeviceOSVersion());
	}

	const char * dtd_appBundle() {
		return dtd_copyPlatformString(dtd_applicationUtils->GetAppBundle());
	}

	const char * dtd_appVersion() {
		return dtd_copyPlatformString(dtd_applicationUtils->GetAppVersion());
	}

	const char * dtd_appBuild() {
		return dtd_copyPlatformString(dtd_applicationUtils->GetAppCodeVersion());
	}

	void dtd_pushInit(DTD_onRegisteredForPushNotifications onRegisteredForPushNotifications, DTD_onFailedToRegisteredForPushNotifications onFailedToRegisteredForPushNotifications,
		DTD_onPushNotificationsReceived onPushNotificationsReceived, DTD_onPushNotificationOpened onPushNotificationOpened) {
		dtd_onRegisteredForPushNotifications = onRegisteredForPushNotifications;
		dtd_onFailedToRegisteredForPushNotifications = onFailedToRegisteredForPushNotifications;
		dtd_onPushNotificationsReceived = onPushNotificationsReceived;
		dtd_onPushNotificationOpened = onPushNotificationOpened;
		dtd_pushClient->OnPushTokenReceived += ref new com::devtodev::extension::metro::Push::OnPushTokenReceivedHandler(&OnPushTokenReceived);
		dtd_pushClient->OnPushTokenFailed += ref new com::devtodev::extension::metro::Push::OnPushTokenFailedHandler(&OnPushTokenFailed);
		dtd_pushClient->OnPushReceived += ref new com::devtodev::extension::metro::Push::OnPushReceivedHandler(&OnOnPushReceived);
		dtd_pushClient->SetOnPushOpened(ref new com::devtodev::extension::metro::Push::OnPushOpenedHandler(&OnPushOpened));
		dtd_pushClient->Initialize();
	}

	const char * dtd_pushNativeEvents() {
		return dtd_copyPlatformString(dtd_pushClient->GetNativeEvents());
	}

	const void dtd_pushCloseClient() {
		return dtd_pushClient->Close();
	}

	const void dtd_pushClearData() {
		return dtd_pushClient->ClearNativeEvents();
	}
}
