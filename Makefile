BTOUCH=/Developer/MonoTouch/usr/bin/btouch
SMCS=/Developer/MonoTouch/usr/bin/smcs
MONOXBUILD=/Library/Frameworks/Mono.framework/Commands/xbuild

all: AFNetworking.dll

vendor:
	git submodule update --init --recursive

libAFNetworking.a: vendor
	xcodebuild -project "./vendor/afnetworking/AFNetworking Static Library.xcodeproj" -sdk iphonesimulator -configuration Release clean build
	xcodebuild -project "./vendor/afnetworking/AFNetworking Static Library.xcodeproj" -sdk iphoneos -configuration Release clean build
	lipo -create -output ./vendor/afnetworking/build/libAFNetworking.a ./vendor/afnetworking/build/Release-iphoneos/libAFNetworking.a ./vendor/afnetworking/build/Release-iphonesimulator/libAFNetworking.a

AFNetworking.dll: libAFNetworking.a
	cp ./vendor/afnetworking/build/libAFNetworking.a ./AFNetworking/libAFNetworking.a
	$(MONOXBUILD) /p:Configuration=Release ./AFNetworking/AFNetworking.csproj
	cp ./AFNetworking/bin/Release/AFNetworking.dll ./AFNetworking.dll

clean:
	rm -r vendor
	rm -r ./AFNetworking/bin
	rm -r ./AFNetworking/obj
	rm ./AFNetworking/libAFNetworking.a
	rm ./AFNetworking.dll
