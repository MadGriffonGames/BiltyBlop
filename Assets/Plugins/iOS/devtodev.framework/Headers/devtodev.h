//
//  devtodev.h
//  devtodev
//
//  Created by Aleksey Kornienko on 19/11/15.
//  Copyright © 2015 Aleksey Kornienko. All rights reserved.
//

#import <UIKit/UIKit.h>
#import <UserNotifications/UserNotifications.h>


//! Project version number for devtodev.
FOUNDATION_EXPORT double devtodevVersionNumber;

//! Project version string for devtodev.
FOUNDATION_EXPORT const unsigned char devtodevVersionString[];

// In this header, you should import all the public headers of your framework using statements like #import <devtodev/PublicHeader.h>

extern "C" {
	const char* cCopy(const char* string);
	const char * dtd_a();
	float dtd_b();
	int dtd_c();
	const char * dtd_d();
	const char * dtd_e();
	const char * dtd_f();
	const char * dtd_g();
    const char * dtd_i(const char* key);
    void dtd_j(const char* key);
    void dtd_k();
    const char * dtd_l();
	void dtd_z(const char* appKey);
    void dtd_x(const char * url, const char * postData);
    void dtd_y();
    const char * dtd_s();
    const char * dtd_p();
    void dtd_t(const char* categories);
    void logger_setLogEnabled(bool isEnabled);
    int getTimeZoneOffset();
}
