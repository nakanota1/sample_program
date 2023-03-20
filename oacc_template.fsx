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
    ch.i <| fun size ->
        size <== 1024
        ch.i1 size <| fun a ->
            oacc.parallelize <| fun () ->
                iter.num size <| fun i ->
                    a[i] <== i
