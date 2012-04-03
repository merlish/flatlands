namespace flatlands

open System.Drawing

module Paint =
    let paint (w : int) (h : int) (ps : bool list) =
        let c = ref 0
        let bmp = new Bitmap(w,h)
        let g = Graphics.FromImage(bmp)
        g.Clear(Color.Red)
        g.Dispose()
        let pfun = (fun b -> bmp.SetPixel(!c % w, !c / w, if b then Color.White else Color.Black); c := !c + 1)
        ignore (List.map pfun ps)
        bmp.Save("out.png")

        
        

