#if defined(__has_include) && __has_include("UnityAppController.h")
#import "UnityAppController.h"
#else
#import "EmptyUnityAppController.h"
#endif

#import <Appodeal/Appodeal.h>

#import "AppodealInterstitialDelegate.h"
#import "AppodealNonSkippableVideoDelegate.h"
#import "AppodealBannerDelegate.h"
#import "AppodealBannerViewDelegate.h"
#import "AppodealRewardedVideoDelegate.h"

#import "AppodealUnityBannerView.h"

static AppodealUnityBannerView *bannerUnity;

static UIViewController* RootViewController() {
    return ((UnityAppController *)[UIApplication sharedApplication].delegate).rootViewController;
}

static NSDateFormatter *DateFormatter() {
    static dispatch_once_t onceToken;
    static NSDateFormatter *formatter;
    dispatch_once(&onceToken, ^{
        formatter = [[NSDateFormatter alloc] init];
        formatter.dateFormat = @"dd/MM/yyyy";
    });
    return formatter;
}

void AppodealInitializeWithTypes(const char *apiKey, int types) {
    [Appodeal setFramework:APDFrameworkUnity];
    [Appodeal initializeWithApiKey:[NSString stringWithUTF8String:apiKey] types:types];
}

BOOL AppodealShowAd(int style) {
    return [Appodeal showAd:style rootViewController: RootViewController()];
}

BOOL AppodealShowAdforPlacement(int style, const char *placement) {
    return [Appodeal showAd:style forPlacement:[NSString stringWithUTF8String:placement] rootViewController:RootViewController()];
}

void AppodealSetAutocache(BOOL autocache, int types) {
    [Appodeal setAutocache:autocache types:types];
}

void AppodealCacheAd(int types) {
    [Appodeal cacheAd:types];
}

BOOL AppodealIsReadyWithStyle(int style) {
    return [Appodeal isReadyForShowWithStyle:style];
}

static AppodealBannerViewDelegate *AppodealBannerViewDelegateInstance;
void AppodealSetBannerViewDelegate(AppodealBannerCallbacks bannerViewDidLoadAd,
                                   AppodealBannerCallbacks bannerViewDidFailToLoadAd,
                                   AppodealBannerCallbacks bannerViewDidClick) {
    
    AppodealBannerViewDelegateInstance = [AppodealBannerViewDelegate new];
    
    AppodealBannerViewDelegateInstance.bannerViewDidLoadAdCallback = bannerViewDidLoadAd;
    AppodealBannerViewDelegateInstance.bannerViewDidFailToLoadAdCallback = bannerViewDidFailToLoadAd;
    AppodealBannerViewDelegateInstance.bannerViewDidClickCallback = bannerViewDidClick;
    
    if(!bannerUnity) {
        bannerUnity = [AppodealUnityBannerView sharedInstance];
    }
    [bannerUnity.bannerView setDelegate:AppodealBannerViewDelegateInstance];
}

BOOL AppodealShowBannerAdViewforPlacement(int YAxis, int XAxis, const char *placement) {
    if(!bannerUnity) {
        bannerUnity = [AppodealUnityBannerView sharedInstance];
    }
    [bannerUnity showBannerView:RootViewController() XAxis:XAxis YAxis:YAxis placement:[NSString stringWithUTF8String:placement]];
    return false;
}

void AppodealHideBanner() {
    [Appodeal hideBanner];
}

void AppodealHideBannerView() {
    if(bannerUnity) {
        [bannerUnity.bannerView removeFromSuperview];
    }
}

void AppodealConfirmUsage(int types) {
    [Appodeal confirmUsage:types];
}

static AppodealInterstitialDelegate *AppodealInterstitialDelegateInstance;
void AppodealSetInterstitialDelegate(AppodealInterstitialCallbacks interstitialDidLoadAd,
                                     AppodealInterstitialCallbacks interstitialDidFailToLoadAd,
                                     AppodealInterstitialCallbacks interstitialDidClick,
                                     AppodealInterstitialCallbacks interstitialDidDismiss,
                                     AppodealInterstitialCallbacks interstitialWillPresent) {
    
    AppodealInterstitialDelegateInstance = [AppodealInterstitialDelegate new];
    
    AppodealInterstitialDelegateInstance.interstitialDidLoadCallback = interstitialDidLoadAd;
    AppodealInterstitialDelegateInstance.interstitialDidFailToLoadAdCallback = interstitialDidFailToLoadAd;
    AppodealInterstitialDelegateInstance.interstitialDidClickCallback = interstitialDidClick;
    AppodealInterstitialDelegateInstance.interstitialDidDismissCallback = interstitialDidDismiss;
    AppodealInterstitialDelegateInstance.interstitialWillPresentCallback = interstitialWillPresent;
    
    [Appodeal setInterstitialDelegate:AppodealInterstitialDelegateInstance];
}


static AppodealBannerDelegate *AppodealBannerDelegateInstance;
void AppodealSetBannerDelegate(AppodealBannerCallbacks bannerDidLoadAd,
                               AppodealBannerCallbacks bannerDidFailToLoadAd,
                               AppodealBannerCallbacks bannerDidClick,
                               AppodealBannerCallbacks bannerDidShow) {
    
    AppodealBannerDelegateInstance = [AppodealBannerDelegate new];
    
    AppodealBannerDelegateInstance.bannerDidLoadAdCallback = bannerDidLoadAd;
    AppodealBannerDelegateInstance.bannerDidFailToLoadAdCallback = bannerDidFailToLoadAd;
    AppodealBannerDelegateInstance.bannerDidClickCallback = bannerDidClick;
    AppodealBannerDelegateInstance.bannerDidShowCallback = bannerDidShow;
    
    [Appodeal setBannerDelegate:AppodealBannerDelegateInstance];
}

static AppodealNonSkippableVideoDelegate *AppodealNonSkippableVideoDelegateInstance;
void AppodealSetNonSkippableVideoDelegate(AppodealNonSkippableVideoCallbacks nonSkippableVideoDidLoadAd,
                                          AppodealNonSkippableVideoCallbacks nonSkippableVideoDidFailToLoadAd,
                                          AppodealNonSkippableVideoCallbacks nonSkippableVideoWillDismiss,
                                          AppodealNonSkippableVideoCallbacks nonSkippableVideoDidFinish,
                                          AppodealNonSkippableVideoCallbacks nonSkippableVideoDidPresent) {
    
    AppodealNonSkippableVideoDelegateInstance = [AppodealNonSkippableVideoDelegate new];
    
    AppodealNonSkippableVideoDelegateInstance.nonSkippableVideoDidLoadAdCallback = nonSkippableVideoDidLoadAd;
    AppodealNonSkippableVideoDelegateInstance.nonSkippableVideoDidFailToLoadAdCallback = nonSkippableVideoDidFailToLoadAd;
    AppodealNonSkippableVideoDelegateInstance.nonSkippableVideoWillDismissCallback = nonSkippableVideoWillDismiss;
    AppodealNonSkippableVideoDelegateInstance.nonSkippableVideoDidFinishCallback = nonSkippableVideoDidFinish;
    AppodealNonSkippableVideoDelegateInstance.nonSkippableVideoDidPresentCallback = nonSkippableVideoDidPresent;
    
    [Appodeal setNonSkippableVideoDelegate:AppodealNonSkippableVideoDelegateInstance];
}

static AppodealRewardedVideoDelegate *AppodealRewardedVideoDelegateInstance;
void AppodealSetRewardedVideoDelegate(AppodealRewardedVideoCallbacks rewardedVideoDidLoadAd,
                                      AppodealRewardedVideoCallbacks rewardedVideoDidFailToLoadAd,
                                      AppodealRewardedVideoCallbacks rewardedVideoWillDismiss,
                                      AppodealRewardedVideoDidFinishCallback rewardedVideoDidFinish,
                                      AppodealRewardedVideoCallbacks rewardedVideoDidPresent) {
    
    AppodealRewardedVideoDelegateInstance = [AppodealRewardedVideoDelegate new];
    
    AppodealRewardedVideoDelegateInstance.rewardedVideoDidLoadAdCallback = rewardedVideoDidLoadAd;
    AppodealRewardedVideoDelegateInstance.rewardedVideoDidFailToLoadAdCallback = rewardedVideoDidFailToLoadAd;
    AppodealRewardedVideoDelegateInstance.rewardedVideoWillDismissCallback = rewardedVideoWillDismiss;
    AppodealRewardedVideoDelegateInstance.rewardedVideoDidFinishCallback = rewardedVideoDidFinish;
    AppodealRewardedVideoDelegateInstance.rewardedVideoDidPresentCallback = rewardedVideoDidPresent;
    
    [Appodeal setRewardedVideoDelegate:AppodealRewardedVideoDelegateInstance];
}

char * AppodealGetVersion() {
    const char *cString = [[Appodeal getVersion] UTF8String];
    char *cStringCopy = calloc([[Appodeal getVersion] length]+1, 1);
    return strncpy(cStringCopy, cString, [[Appodeal getVersion] length]);
}

void AppodealDisableLocationPermissionCheck() {
    [Appodeal disableLocationPermissionCheck];
}

void AppodealSetDebugEnabled(BOOL debugEnabled) {
    [Appodeal setDebugEnabled:debugEnabled];
}

void AppodealSetTestingEnabled(BOOL testingEnabled) {
    [Appodeal setTestingEnabled:testingEnabled];
}

void AppodealDisableNetwork(const char * networkName) {
    [Appodeal disableNetworkForAdType:AppodealAdTypeInterstitial name:[NSString stringWithUTF8String:networkName]];
    [Appodeal disableNetworkForAdType:AppodealAdTypeSkippableVideo name:[NSString stringWithUTF8String:networkName]];
    [Appodeal disableNetworkForAdType:AppodealAdTypeBanner name:[NSString stringWithUTF8String:networkName]];
    [Appodeal disableNetworkForAdType:AppodealAdTypeRewardedVideo name:[NSString stringWithUTF8String:networkName]];
    [Appodeal disableNetworkForAdType:AppodealAdTypeNonSkippableVideo name:[NSString stringWithUTF8String:networkName]];
}

void AppodealDisableNetworkForAdTypes(const char * networkName, int type) {
    [Appodeal disableNetworkForAdType:type name:[NSString stringWithUTF8String:networkName]];
}

void setCustomSegmentBool(const char *name, BOOL value)
{
    NSString *ValueFromBOOL;
    if(value) {
        ValueFromBOOL = @"YES";
    } else {
        ValueFromBOOL = @"NO";
    }
    
    NSDictionary *tempDictionary = @{[NSString stringWithUTF8String:name]: ValueFromBOOL};
    NSDictionary *dict =  [NSDictionary dictionaryWithDictionary:tempDictionary];
    [Appodeal setCustomRule:dict];
}

void setCustomSegmentInt(const char *name, int value)
{
    NSDictionary *tempDictionary = @{[NSString stringWithUTF8String:name]: [NSNumber numberWithInt:value]};
    NSDictionary *dict =  [NSDictionary dictionaryWithDictionary:tempDictionary];
    [Appodeal setCustomRule:dict];
}

void setCustomSegmentDouble(const char *name, double value)
{
    NSDictionary *tempDictionary = @{[NSString stringWithUTF8String:name]: [NSNumber numberWithDouble:value]};
    NSDictionary *dict =  [NSDictionary dictionaryWithDictionary:tempDictionary];
    [Appodeal setCustomRule:dict];
}

void setCustomSegmentString(const char *name, const char *value) {
    NSDictionary *tempDictionary = @{[NSString stringWithUTF8String:name]: [NSString stringWithUTF8String:value]};
    NSDictionary *dict =  [NSDictionary dictionaryWithDictionary:tempDictionary];
    [Appodeal setCustomRule:dict];
}

void setSmartBanners(bool value) {
    [Appodeal setSmartBannersEnabled:value];
}

void setBannerBackground(BOOL value) {
    [Appodeal setBannerBackgroundVisible:value];
}

void setBannerAnimation(BOOL value) {
    [Appodeal setBannerAnimationEnabled:value];
}

void trackInAppPurchase(int amount, const char * currency) {
    [[APDSdk sharedSdk] trackInAppPurchase:[NSNumber numberWithInt:amount] currency:[NSString stringWithUTF8String:currency]];
}

void AppodealSetUserAge(int age) {
    [Appodeal setUserAge:age];
}

void AppodealSetUserGender(int gender) {
    [Appodeal setUserGender:gender];
}
