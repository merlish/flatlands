namespace flatlands

open System.Drawing

module Paint =
    let paint (w : int) (h : int) (ps : bool list) =
        let c = ref 0
        let bmp = new Bitmap(w,h)
        let g = Graphics.FromImage(bmp)
        g.Clear(Color.Red)
        g.Dispose()
        let pfun = (fun b -> if !c < w*h then (bmp.SetPixel(!c % w, !c / w, if b then Color.White else Color.Black); c := !c + 1) else (printfn "too many pixels (tried to draw %d %d)" (!c % w) (!c / w)))
        ignore (List.map pfun ps)
        bmp.Save("out.png")

        
        

