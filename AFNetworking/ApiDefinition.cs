using System;
using System.Drawing;
//using System.ComponentModel;

using MonoTouch.ObjCRuntime;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace AFNetworking
{
	public delegate void AFHttpRequestSuccessCallback(AFHTTPRequestOperation operation, NSObject responseObject);

	public delegate void AFHttpRequestFailureCallback(AFHTTPRequestOperation operation, NSError error);

	public delegate void AFImageRequestCallback(UIImage image);

	public delegate UIImage AFImageRequestImageProcessingCallback(UIImage image);

	public delegate void AFImageRequestDetailedCallback(NSUrlRequest request, NSHttpUrlResponse response, UIImage image);

	public delegate void AFImageRequestFailedCallback(NSUrlRequest request, NSHttpUrlResponse response, NSError error);

	[BaseType (typeof (NSObject))]
	public partial interface AFHTTPClient {
		
		[Export ("baseURL")]
		NSUrl BaseURL { get; }
		
		[Export ("stringEncoding")]
		NSStringEncoding StringEncoding { get; set; }

		[Export ("parameterEncoding")]
		AFHTTPClientParameterEncoding ParameterEncoding { get; set; }
		
		[Export ("operationQueue")]
		NSOperationQueue OperationQueue { get; }
		
		[Static, Export ("clientWithBaseURL:")]
		AFHTTPClient ClientWithBaseURL (NSUrl url);
		
		[Export ("initWithBaseURL:")]
		NSObject Constructor (NSUrl url);
		
		[Export ("registerHTTPOperationClass:")]
		bool RegisterHTTPOperationClass (Class operationClass);
		
		[Export ("unregisterHTTPOperationClass:")]
		void UnregisterHTTPOperationClass (Class operationClass);
		
		[Export ("defaultValueForHeader:")]
		string DefaultValueForHeader (string header);
		
		[Export ("setDefaultHeader:value:")]
		void SetDefaultHeader (string header, string value);
		
		[Export ("setAuthorizationHeaderWithUsername:password:")]
		void SetAuthorizationHeaderWithUsername (string username, string password);
		
		[Export ("setAuthorizationHeaderWithToken:")]
		void SetAuthorizationHeaderWithToken (string token);
		
		[Export ("clearAuthorizationHeader")]
		void ClearAuthorizationHeader ();
		
		[Export ("setDefaultCredential:")]
		void SetDefaultCredential (NSUrlCredential credential);
		
		[Export ("requestWithMethod:path:parameters:")]
		NSMutableUrlRequest RequestWithMethod (string method, string path, [NullAllowed] NSDictionary parameters);
		
		/*[Export ("multipartFormRequestWithMethod:path:parameters:constructingBodyWithBlock:")]
		NSMutableURLRequest MultipartFormRequestWithMethod (string method, string path, NSDictionary parameters, [unmapped: blockpointer: BlockPointer] block);
		
		[Export ("HTTPRequestOperationWithRequest:success:failure:")]
		AFHTTPRequestOperation HTTPRequestOperationWithRequest (NSURLRequest urlRequest, [unmapped: blockpointer: BlockPointer] success, [unmapped: blockpointer: BlockPointer] failure);*/
		
		[Export ("enqueueHTTPRequestOperation:")]
		void EnqueueHTTPRequestOperation (AFHTTPRequestOperation operation);
		
		[Export ("cancelAllHTTPOperationsWithMethod:path:")]
		void CancelAllHTTPOperationsWithMethod (string method, string path);
		
		/*[Export ("enqueueBatchOfHTTPRequestOperationsWithRequests:progressBlock:completionBlock:")]
		void EnqueueBatchOfHTTPRequestOperationsWithRequests (NSArray urlRequests, [unmapped: blockpointer: BlockPointer] progressBlock, [unmapped: blockpointer: BlockPointer] completionBlock);
		
		[Export ("enqueueBatchOfHTTPRequestOperations:progressBlock:completionBlock:")]
		void EnqueueBatchOfHTTPRequestOperations (NSArray operations, [unmapped: blockpointer: BlockPointer] progressBlock, [unmapped: blockpointer: BlockPointer] completionBlock);*/
		
		[Export ("getPath:parameters:success:failure:")]
		void GetPath (string path, [NullAllowed] NSDictionary parameters, Action<AFHTTPRequestOperation, NSObject> success, [NullAllowed] Action<AFHTTPRequestOperation, NSError> failure);
		
		/*[Export ("postPath:parameters:success:failure:")]
		void PostPath (string path, NSDictionary parameters, [unmapped: blockpointer: BlockPointer] success, [unmapped: blockpointer: BlockPointer] failure);
		
		[Export ("putPath:parameters:success:failure:")]
		void PutPath (string path, NSDictionary parameters, [unmapped: blockpointer: BlockPointer] success, [unmapped: blockpointer: BlockPointer] failure);
		
		[Export ("deletePath:parameters:success:failure:")]
		void DeletePath (string path, NSDictionary parameters, [unmapped: blockpointer: BlockPointer] success, [unmapped: blockpointer: BlockPointer] failure);
		
		[Export ("patchPath:parameters:success:failure:")]
		void PatchPath (string path, NSDictionary parameters, [unmapped: blockpointer: BlockPointer] success, [unmapped: blockpointer: BlockPointer] failure);*/
		
		/*[Field ("kAFUploadStream3GSuggestedPacketSize", "__Internal")]
		uint kAFUploadStream3GSuggestedPacketSize { get; }*/
		
		[Field ("kAFUploadStream3GSuggestedDelay", "__Internal")]
		double kAFUploadStream3GSuggestedDelay { get; }
	}
	
	[Model]
	public partial interface AFMultipartFormData {
		
		[Export ("appendPartWithFileURL:name:error:")]
		bool AppendPartWithFileURL (NSUrl fileURL, string name, out NSError error);
		
		[Export ("appendPartWithFileURL:name:fileName:mimeType:error:")]
		bool AppendPartWithFileURL (NSUrl fileURL, string name, string fileName, string mimeType, out NSError error);
		
		[Export ("appendPartWithFileData:name:fileName:mimeType:")]
		void AppendPartWithFileData (NSData data, string name, string fileName, string mimeType);
		
		[Export ("appendPartWithFormData:name:")]
		void AppendPartWithFormData (NSData data, string name);
		
		[Export ("appendPartWithHeaders:body:")]
		void AppendPartWithHeaders (NSDictionary headers, NSData body);
		
		[Export ("throttleBandwidthWithPacketSize:delay:")]
		void ThrottleBandwidthWithPacketSize (uint numberOfBytes, double delay);
	}

	[BaseType (typeof (AFURLConnectionOperation))]
	public partial interface AFHTTPRequestOperation {

		[Export("initWithRequest:")]
		IntPtr Constructor(NSUrlRequest request);

		/*[Export ("response")]
		NSUrlResponse Response { get; }*/
		
		[Export ("hasAcceptableStatusCode")]
		bool HasAcceptableStatusCode { get; }
		
		[Export ("hasAcceptableContentType")]
		bool HasAcceptableContentType { get; }
		
		[Export ("successCallbackQueue")]
		MonoTouch.CoreFoundation.DispatchQueue SuccessCallbackQueue { get; set; }
		
		[Export ("failureCallbackQueue")]
		MonoTouch.CoreFoundation.DispatchQueue FailureCallbackQueue { get; set; }
		
		[Static, Export ("acceptableStatusCodes")]
		NSIndexSet AcceptableStatusCodes ();
		
		[Static, Export ("addAcceptableStatusCodes:")]
		void AddAcceptableStatusCodes (NSIndexSet statusCodes);
		
		[Static, Export ("acceptableContentTypes")]
		NSSet AcceptableContentTypes ();
		
		[Static, Export ("addAcceptableContentTypes:")]
		void AddAcceptableContentTypes (NSSet contentTypes);
		
		[Static, Export ("canProcessRequest:")]
		bool CanProcessRequest (NSUrlRequest urlRequest);
		
		[Export ("setCompletionBlockWithSuccess:failure:")]
		void SetCompletionBlockWithSuccess(AFHttpRequestSuccessCallback success, AFHttpRequestFailureCallback failure);
	}
	
	[BaseType (typeof (AFHTTPRequestOperation))]
	public partial interface AFImageRequestOperation {
		
		[Export ("responseImage")]
		UIImage ResponseImage { get; }
		
		[Static, Export ("imageRequestOperationWithRequest:success:")]
		AFImageRequestOperation ImageRequestOperationWithRequest (NSUrlRequest urlRequest, AFImageRequestCallback success);
		
		[Static, Export ("imageRequestOperationWithRequest:imageProcessingBlock:success:failure:")]
		AFImageRequestOperation ImageRequestOperationWithRequest(NSUrlRequest urlRequest, AFImageRequestImageProcessingCallback imageProcessingBlock, AFImageRequestDetailedCallback success, AFImageRequestFailedCallback failed);
	}
	
	[BaseType (typeof (AFHTTPRequestOperation))]
	public partial interface AFJSONRequestOperation {
		
		[Export ("responseJSON")]
		NSObject ResponseJSON { get; }
		
		/*[Export ("JSONReadingOptions")]
		NSJSONReadingOptions JSONReadingOptions { get; set; }*/
		
		/*[Static, Export ("JSONRequestOperationWithRequest:success:failure:")]
		instancetype JSONRequestOperationWithRequest (NSURLRequest urlRequest, [unmapped: blockpointer: BlockPointer] success, [unmapped: blockpointer: BlockPointer] failure);*/
	}

	[BaseType (typeof (AFHTTPRequestOperation))]
	public partial interface AFPropertyListRequestOperation {
		
		[Export ("responsePropertyList")]
		NSObject ResponsePropertyList { get; }
		
		[Export ("propertyListReadOptions")]
		NSPropertyListReadOptions PropertyListReadOptions { get; set; }
		
		/*[Static, Export ("propertyListRequestOperationWithRequest:success:failure:")]
		instancetype PropertyListRequestOperationWithRequest (NSURLRequest urlRequest, [unmapped: blockpointer: BlockPointer] success, [unmapped: blockpointer: BlockPointer] failure);*/
	}
	
	[BaseType (typeof (NSOperation))]
	public partial interface AFURLConnectionOperation {
		
		[Export ("runLoopModes")]
		NSSet RunLoopModes { get; set; }
		
		[Export ("request")]
		NSUrlRequest Request { get; }
		
		[Export ("response")]
		NSUrlResponse Response { get; }
		
		[Export ("error")]
		NSError Error { get; }
		
		[Export ("responseData")]
		NSData ResponseData { get; }
		
		[Export ("responseString")]
		string ResponseString { get; }
		
		[Export ("responseStringEncoding")]
		NSStringEncoding ResponseStringEncoding { get; }
		
		[Export ("shouldUseCredentialStorage")]
		bool ShouldUseCredentialStorage { get; set; }
		
		[Export ("credential")]
		NSUrlCredential Credential { get; set; }
		
		[Export ("inputStream")]
		NSInputStream InputStream { get; set; }
		
		[Export ("outputStream")]
		NSOutputStream OutputStream { get; set; }
		
		[Export ("userInfo")]
		NSDictionary UserInfo { get; set; }
		
		[Export ("initWithRequest:")]
		IntPtr Constructor (NSUrlRequest urlRequest);
		
		[Export ("pause")]
		void Pause ();
		
		[Export ("isPaused")]
		bool IsPaused ();
		
		[Export ("resume")]
		void Resume ();
		
		[Export ("setCompletionBlock:")]
		void SetCompletionBlock(Action block);
		
		/*[Export ("setUploadProgressBlock:")]
		void SetUploadProgressBlock ([unmapped: blockpointer: BlockPointer] block);
		
		[Export ("setDownloadProgressBlock:")]
		void SetDownloadProgressBlock ([unmapped: blockpointer: BlockPointer] block);
		
		[Export ("setAuthenticationAgainstProtectionSpaceBlock:")]
		void SetAuthenticationAgainstProtectionSpaceBlock ([unmapped: blockpointer: BlockPointer] block);
		
		[Export ("setAuthenticationChallengeBlock:")]
		void SetAuthenticationChallengeBlock ([unmapped: blockpointer: BlockPointer] block);
		
		[Export ("setRedirectResponseBlock:")]
		void SetRedirectResponseBlock ([unmapped: blockpointer: BlockPointer] block);
		
		[Export ("setCacheResponseBlock:")]
		void SetCacheResponseBlock ([unmapped: blockpointer: BlockPointer] block);*/
		
		[Field ("AFNetworkingErrorDomain", "__Internal")]
		NSString AFNetworkingErrorDomain { get; }
		
		[Field ("AFNetworkingOperationFailingURLRequestErrorKey", "__Internal")]
		NSString AFNetworkingOperationFailingURLRequestErrorKey { get; }
		
		[Field ("AFNetworkingOperationFailingURLResponseErrorKey", "__Internal")]
		NSString AFNetworkingOperationFailingURLResponseErrorKey { get; }
		
		[Notification, Field ("AFNetworkingOperationDidStartNotification", "__Internal")]
		NSString AFNetworkingOperationDidStartNotification { get; }
		
		[Notification, Field ("AFNetworkingOperationDidFinishNotification", "__Internal")]
		NSString AFNetworkingOperationDidFinishNotification { get; }
	}
	
	[BaseType (typeof (AFHTTPRequestOperation))]
	public partial interface AFXMLRequestOperation {
		
		/*[Export ("responseXMLParser")]
		NSXMLParser ResponseXMLParser { get; }
		
		[Export ("responseXMLDocument")]
		NSXMLDocument ResponseXMLDocument { get; }*/
		
		/*[Static, Export ("XMLParserRequestOperationWithRequest:success:failure:")]
		instancetype XMLParserRequestOperationWithRequest (NSURLRequest urlRequest, [unmapped: blockpointer: BlockPointer] success, [unmapped: blockpointer: BlockPointer] failure);
		
		[Static, Export ("XMLDocumentRequestOperationWithRequest:success:failure:")]
		instancetype XMLDocumentRequestOperationWithRequest (NSURLRequest urlRequest, [unmapped: blockpointer: BlockPointer] success, [unmapped: blockpointer: BlockPointer] failure);*/
	}

	[BaseType (typeof (UIImageView))]
	[Category]
	interface AFNetworkingImageExtras {
		[Export ("setImageWithURL:")]
		void SetImageUrl (NSUrl url);

		[Export ("setImageWithURL:placeholderImage:")]
		void SetImageUrl(NSUrl url, UIImage placeholderImage);
	}

}

