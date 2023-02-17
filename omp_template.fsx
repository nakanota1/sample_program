//#############################################################################
// project title
let projectname = "omp_template"
let version = "0.0.0"
//#############################################################################
 
let outputdir = @"C:\cygwin64\home\work"
 
#I "C:\\Aqualis\\lib\\180_0_4_0"
#r "Aqualis.dll"
#load "version.fsx"
 
let fullversion = preprocess.backup outputdir __SOURCE_DIRECTORY__ __SOURCE_FILE__ projectname version
 
open Aqualis

let sub(a:num1) = 
    iter.num a.size1 <| fun i ->
        a[i] <== i / 100

let sub2(w:num1, a:int, b:int) =
    iter.range a b <| fun i ->
        w[i] <== i

let add(sum:num0, a:int, b:int) =
    iter.range a b <| fun i ->
        sum <== sum + i
 
Compile [F] outputdir projectname fullversion <| fun () ->
    //ch.d1 100 <| fun a ->
    //    omp.parallelize <| fun () ->
    //        sub (a)

    //    omp.parallelize_th 4 <| fun () ->
    //        sub (a)

    ch.i <| fun sum ->
        //sum <== 0
        //omp.reduction sum "+" <| fun () ->
        //    add (sum,1,100)

        sum <== 0
        ch.i1 100 <| fun w ->
            omp.sections 4 <| fun () ->
                omp.section <| fun () ->
                    sub2 (w,1,25)
                omp.section <| fun () ->
                    sub2 (w,26,50)
                omp.section <| fun () ->
                    sub2 (w,51,75)
                omp.section <| fun () ->
                    sub2 (w,76,100)
            iter.num w.size1 <| fun i ->
                sum <== sum + w[i]
            print.c sum
