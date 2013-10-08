AFNetworking-Sharp
==================

Wrapper to the great [AFNetworking](https://github.com/AFNetworking/AFNetworking) library for Xamarin iOS.

Lots of methods aren't fully supported yet, did enough to port the example app over to C#.

## Building
-----

To build `AFNetworking.dll` run the following from the commandline on your Mac

```sh
make
```

There are a couple other commands you can run in case you only want to do specific things

*Pulling down AFNetworking code:*

```sh
make vendor
```

*Building libAFNetworking.a:* 

```sh
make libAFNetworking.a
```

*Cleaning up after `make`:*

```sh
make clean
```
