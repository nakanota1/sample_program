//#############################################################################
// project title
let projectname = "oacc_template"
let version = "0.0.0"
//#############################################################################
 
let outputdir = @"C:\cygwin64\home\work"
 
#I "C:\\Aqualis\\lib\\180_0_4_0"
#r "Aqualis.dll"
#load "version.fsx"
 
let fullversion = preprocess.backup outputdir __SOURCE_DIRECTORY__ __SOURCE_FILE__ projectname version
 
open Aqualis

 
Compile [F] outputdir projectname fullversion <| fun () ->
//-------------------------------------------------
//データ転送する変数を宣言
//ch.copyin_i <| fun (変数名) -> //CPU -> GPUに転送
//ch.copyout_i <| fun (変数名) -> //GPU -> CPUに転送
//通常の変数宣言に"copyin","copyout"がついたのみで後は同じ
//-------------------------------------------------
    ch.copyin_i1 1024 <| fun a ->
        iter.num a.size1 <| fun i ->
            a[i] <== i

//-------------------------------------------------
//並列化
//oacc.parallelize <| fun () ->
//    (並列領域)
//-------------------------------------------------
    ch.i <| fun size ->
        size <== 1024
        ch.i1 size <| fun a ->
            oacc.parallelize <| fun () ->
                iter.num size <| fun i ->
                    a[i] <== i
