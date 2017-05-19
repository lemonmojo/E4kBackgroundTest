#import "ViewController.h"
#import <E4kBackgroundTestLibrary/E4kBackgroundTestLibrary.h>

@implementation ViewController

- (IBAction)buttonForeground_action:(id)sender
{
    [self testE4k];
}

- (IBAction)buttonBackground_action:(id)sender
{
    dispatch_async(dispatch_get_global_queue(DISPATCH_QUEUE_PRIORITY_DEFAULT, 0), ^{
        [self testE4k];
    });
}

- (void)testE4k
{
    E4kBackgroundTestLibrary_TestClass *testClass = [E4kBackgroundTestLibrary_TestClass new];
    testClass.name = @"Embeddinator";
    
    NSLog(@"%@", testClass.name);
}

@end
