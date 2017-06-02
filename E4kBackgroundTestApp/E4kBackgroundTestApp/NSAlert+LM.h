#import <Cocoa/Cocoa.h>

@interface NSAlert (LM)

+ (NSModalResponse)runModalAlertWithStyle:(NSAlertStyle)alertStyle
                              messageText:(NSString*)messageText
                          informativeText:(NSString*)informativeText;

+ (NSModalResponse)runModalAlertWithStyle:(NSAlertStyle)alertStyle
                              messageText:(NSString*)messageText
                          informativeText:(NSString*)informativeText
                         firstButtonTitle:(NSString*)firstButtonTitle;

+ (NSModalResponse)runModalAlertWithStyle:(NSAlertStyle)alertStyle
                              messageText:(NSString*)messageText
                          informativeText:(NSString*)informativeText
                         firstButtonTitle:(NSString*)firstButtonTitle
                        secondButtonTitle:(NSString*)secondButtonTitle;

@end
