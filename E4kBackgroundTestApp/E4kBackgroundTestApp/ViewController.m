#import "ViewController.h"
#import "NSAlert+LM.h"
#import <E4kBackgroundTestLibrary/E4kBackgroundTestLibrary.h>

@implementation ViewController {
    BOOL _didAwake;
    
    __weak IBOutlet NSTextField *textFieldURL;
    __strong E4kBackgroundTestLibrary_Downloader *_downloader;
}

- (void)awakeFromNib
{
    if (_didAwake) {
        return;
    }
    
    _didAwake = YES;
    
    [self subscribeToEvents];
}

- (void)subscribeToEvents
{
    NSNotificationCenter* notificationCenter = [NSNotificationCenter defaultCenter];
    
    [notificationCenter addObserver:self
                           selector:@selector(willStartDownload:)
                               name:E4kBackgroundTestLibrary_Downloader.willStartDownload.name
                             object:nil];
    
    [notificationCenter addObserver:self
                           selector:@selector(didFinishDownload:)
                               name:E4kBackgroundTestLibrary_Downloader.didFinishDownload.name
                             object:nil];
}

- (IBAction)buttonDownload_action:(id)sender
{
    NSString* url = textFieldURL.stringValue;
    
    _downloader = [[E4kBackgroundTestLibrary_Downloader alloc] initWithUrl:url];
    [_downloader download];
}

- (void)willStartDownload:(NSNotification*)aNotification
{
    id<E4kBackgroundTestLibrary_IDownloaderWillStartEventArgs> eventArgs = aNotification.object;
    
    NSModalResponse resp = [NSAlert runModalAlertWithStyle:NSAlertStyleWarning
                                               messageText:@"Download"
                                           informativeText:[NSString stringWithFormat:@"Are you sure you want to continue downloading from %@?", eventArgs.url]
                                          firstButtonTitle:@"Yes, continue"
                                         secondButtonTitle:@"No, cancel"];
    
    BOOL cancel = resp == NSAlertSecondButtonReturn;
    
    eventArgs.cancel = cancel;
}

- (void)didFinishDownload:(NSNotification*)aNotification
{
    id<E4kBackgroundTestLibrary_IDownloaderDidFinishEventArgs> eventArgs = aNotification.object;
    
    if (eventArgs.cancelled) {
        [NSAlert runModalAlertWithStyle:NSAlertStyleWarning
                            messageText:@"Download"
                        informativeText:[NSString stringWithFormat:@"The download from %@ was cancelled by the user.", eventArgs.url]];
        
        return;
    }
    
    if (eventArgs.errorMessage) {
        [NSAlert runModalAlertWithStyle:NSAlertStyleWarning
                            messageText:@"Download"
                        informativeText:[NSString stringWithFormat:@"An error occurred while downloading from %@: %@", eventArgs.url, eventArgs.errorMessage]];
        
        return;
    }
    
    [NSAlert runModalAlertWithStyle:NSAlertStyleInformational
                        messageText:@"Download"
                    informativeText:[NSString stringWithFormat:@"Successfully downloaded from %@: %@", eventArgs.url, eventArgs.result]];
}

@end
