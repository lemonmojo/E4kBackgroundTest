#import "NSAlert+LM.h"

@implementation NSAlert (LM)

+ (NSModalResponse)runModalAlertWithStyle:(NSAlertStyle)alertStyle
                              messageText:(NSString*)messageText
                          informativeText:(NSString*)informativeText
{
    return [self.class runModalAlertWithStyle:alertStyle
                                  messageText:messageText
                              informativeText:informativeText
                             firstButtonTitle:@"OK"
                            secondButtonTitle:nil];
}

+ (NSModalResponse)runModalAlertWithStyle:(NSAlertStyle)alertStyle
                              messageText:(NSString*)messageText
                          informativeText:(NSString*)informativeText
                         firstButtonTitle:(NSString*)firstButtonTitle
{
    return [self.class runModalAlertWithStyle:alertStyle
                                  messageText:messageText
                              informativeText:informativeText
                             firstButtonTitle:firstButtonTitle
                            secondButtonTitle:nil];
}

+ (NSModalResponse)runModalAlertWithStyle:(NSAlertStyle)alertStyle
                              messageText:(NSString*)messageText
                          informativeText:(NSString*)informativeText
                         firstButtonTitle:(NSString*)firstButtonTitle
                        secondButtonTitle:(NSString*)secondButtonTitle
{
    NSAlert* alert = [NSAlert new];
    alert.alertStyle = alertStyle;
    alert.messageText = messageText;
    alert.informativeText = informativeText;
    
    [alert addButtonWithTitle:firstButtonTitle];
    
    if (secondButtonTitle) {
        [alert addButtonWithTitle:secondButtonTitle];
    }
    
    return [alert runModal];
}

@end
