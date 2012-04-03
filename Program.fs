namespace flatlands

open System
open System.Net
open System.Net.Sockets

module Program =

    type Vector = float * float * float

    type BoundingBox = { min : Vector; max : Vector; }

    //type Ray = { origin : Vector; endpoint : Vector; }
    type Ray = Vector * Vector  // origin, endpoint
    //type RaycastResult = (float) // distance at which ray hits object (or -1)
    type RaycastResult = (bool) // did the ray hit the object?

    type RaycastFunc = Ray -> RaycastResult
    type Geometry = RaycastFunc * BoundingBox

    let interpRay z x0 y0 z0 x1 y1 z1 =
        let zi = (z - z0) / (z1 - z0)
        let xm, ym = x1 - x0, y1 - y0
        (x0 + xm * zi, y0 + ym * zi)
        

    let cubeTest (minx,miny,minz) (maxx,maxy,maxz) (x0,y0,z0) (x1,y1,z1) =
        let (xf,yf) = interpRay minz x0 y0 z0 x1 y1 z1
        let (xb,yb) = interpRay maxz x0 y0 z0 x1 y1 z1

        let (frontleft,frontright,frontup,frontdown) = (xf < minx, xf > maxx, yf < miny, yf > maxy)
        let (backleft,backright,backup,backdown) = (xb < minx, xb > maxx, yb < miny, yb > maxy)

        let fail = ((frontleft && backleft) || (frontup && backup) || (frontright && backright) || (frontdown && backdown))

        (not fail)
        //if ((frontleft && backleft) || (frontup && backup) || (frontright && backright) || (frontdown && backdown))
            //(false)
        //else
            //(true)

    let tripleMap (f : float -> float) (x,y,z) = (f x, f y, f z)

    let cube origin size : Geometry = 
        let sd = size / 2.0
        let min = tripleMap (fun ff -> ff - (size/2.0)) origin
        let max = tripleMap (fun ff -> ff + (size+2.0)) origin
        let rf : RaycastFunc = fun (v0,v1) -> cubeTest min max v0 v1
        (rf, { min = min; max = max })

    [<EntryPoint>]
    let main args =
        printfn "hi there!"
        Paint.paint 640 400 [true; true; true; true; true; true; true; true;]
        0

