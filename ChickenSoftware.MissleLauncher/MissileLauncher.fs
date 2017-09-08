namespace ChickenSoftware.WeaponSystems

open System
open System.Threading
open UsbLibrary

type public MissileLauncher() = 
    let usbPort = new UsbHidPort()
    let handle = new IntPtr()
    let mutable devicePresent = false
    member this.UP = Array.append [|byte(0);byte(2);byte(2)|] (Array.zeroCreate 7)
    member this.DOWN = Array.append [|byte(0);byte(2);byte(1)|] (Array.zeroCreate 7)
    member this.LEFT = Array.append [|byte(0);byte(2);byte(4)|] (Array.zeroCreate 7)
    member this.RIGHT = Array.append [|byte(0);byte(2);byte(8)|] (Array.zeroCreate 7)
    member this.FIRE = Array.append [|byte(0);byte(2);byte(0x10)|] (Array.zeroCreate 7)
    member this.STOP = Array.append [|byte(0);byte(2);byte(0x20)|] (Array.zeroCreate 7)
    member this.LED_ON = Array.append [|byte(0);byte(3);byte(1)|] (Array.zeroCreate 6)
    member this.LED_OFF = Array.append [|byte(0);byte(3)|] (Array.zeroCreate 7)

    member this.usb_OnSpecifiedDeviceArrived(args) =
        devicePresent <- true

    member this.usb_OnSpecifiedDeviceRemoved(args) =
        devicePresent <- false

    member this.usb_OnDataReceived(args:DataRecievedEventArgs) =
        ()
    
    member this.Activate() =
        usbPort.ProductId <- 0
        usbPort.SpecifiedDevice <- null
        usbPort.VendorId <-0
        usbPort.OnSpecifiedDeviceArrived.Add(this.usb_OnSpecifiedDeviceArrived)
        usbPort.OnSpecifiedDeviceRemoved.Add(this.usb_OnSpecifiedDeviceRemoved)
        usbPort.OnDataRecieved.Add(this.usb_OnDataReceived)
        usbPort.VID_List.[0] <- 0xa81
        usbPort.PID_List.[0] <- 0x701
        usbPort.VID_List.[1] <- 0x2123
        usbPort.PID_List.[1] <- 0x1010
        usbPort.ID_List_Cnt <-2
        usbPort.RegisterHandle(handle)

    member this.sendUSBData(data) =
        usbPort.SpecifiedDevice.SendData(data)

    member this.SwitchLed(turnOn) =
        match devicePresent, turnOn with
        | true,true -> this.sendUSBData(this.LED_ON)
        | true,false -> this.sendUSBData(this.LED_OFF)
        | _ -> ()

    member this.moveMissleLauncher(data, interval:int) =
        if devicePresent then
            this.SwitchLed(true)
            this.sendUSBData(data)
            Thread.Sleep(interval)
            this.sendUSBData(this.STOP)
            this.SwitchLed(false)

    member this.Reset() =
        if devicePresent then
            this.moveMissleLauncher(this.LEFT,5500)
            this.moveMissleLauncher(this.RIGHT,2750)
            this.moveMissleLauncher(this.UP,2000)
            this.moveMissleLauncher(this.DOWN,500)
    
    member this.Stop() =
        this.sendUSBData(this.STOP)                
        
    member this.Right(duration) =
        this.moveMissleLauncher(this.RIGHT,duration)                
            
    member this.Left(duration) =
        this.moveMissleLauncher(this.LEFT,duration)                

    member this.Up(duration) =
        this.moveMissleLauncher(this.UP,duration)                
        
    member this.Down(duration) =
        this.moveMissleLauncher(this.DOWN,duration)                

    member this.Fire() =
        this.moveMissleLauncher(this.FIRE,5000)                



   