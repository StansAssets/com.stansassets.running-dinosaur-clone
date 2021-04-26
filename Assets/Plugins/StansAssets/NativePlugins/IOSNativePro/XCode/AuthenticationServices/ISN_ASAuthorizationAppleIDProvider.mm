//
//  ISN_ASAuthorizationAppleIDProvider.m
//  Unity-iPhone
//
//  Created by Stanislav Osipov on 2020-01-27.
//

#import "ISN_Foundation.h"

#import <Foundation/Foundation.h>
#import <AuthenticationServices/AuthenticationServices.h>


extern "C" {
    unsigned long _ISN_ASAuthorizationAppleIDProvider_init() {
        if (@available(iOS 13.0, *)) {
            return [ISN_HashStorage Add:[[ASAuthorizationAppleIDProvider alloc] init]];
        } else {
            return [ISN_HashStorage NullObjectHash];
        }
    }

    unsigned long _ISN_ASAuthorizationAppleIDProvider_createRequest(unsigned long hash) {
       if (@available(iOS 13.0, *)) {
           ASAuthorizationAppleIDProvider* provider = (ASAuthorizationAppleIDProvider*) [ISN_HashStorage Get:hash];
           return [ISN_HashStorage Add:[provider createRequest]];
       } else {
           return [ISN_HashStorage NullObjectHash];
       }
   }
}
