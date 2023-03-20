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

//配列の代入
let sub(a:num1) = 
    iter.num a.size1 <| fun i ->
        a[i] <== i / 100

//a~bのインデックスに変数を代入
//let sub2(w:num1, a:int, b:int) =
//    iter.range a b <| fun i ->
//        w[i] <== i

//sum,a,a+1,a+2,...,a+b-1,a+bの和を計算
//let add(sum:num0, a:int, b:int) =
//    iter.range a b <| fun i ->
//        sum <== sum + i
 
Compile [F] outputdir projectname fullversion <| fun () ->
//-------------------------------------------------
//プライベート変数の宣言
//ch.private_i <| fun (変数名) ->
//i: 整数, d: 小数, z: 複素数
//通常の変数宣言に"private"がついたのみでそれ以外は同じ
//-------------------------------------------------
    ch.private_i <| fun num ->
        num <== 0

//-------------------------------------------------
//並列化
//omp.parallelize <| fun ->
//    (並列領域)
//-------------------------------------------------
    ch.d1 100 <| fun a ->
        omp.parallelize <| fun () ->
            sub (a)

//-------------------------------------------------
//指定のスレッド数で並列化
//omp.parallelize (スレッド数) <| fun () ->
//-------------------------------------------------
    //    omp.parallelize_th 4 <| fun () ->
    //        sub (a)

    //ch.i <| fun sum ->
    //    sum <== 0

//-------------------------------------------------
//和の計算を並列化
//omp.parallelize (変数名) (演算子) <| fun () ->
//-------------------------------------------------
    //    omp.reduction sum "+" <| fun () ->
    //        add (sum,1,100)

//-------------------------------------------------
//複数の処理を別々のスレッドで実行
//omp.sections (スレッド数) <| fun () ->
//    omp.section <| fun () ->
//        (処理1)
//    omp.section <| fun () ->
//        (処理2)
//    ...
//-------------------------------------------------
    //    sum <== 0
    //    ch.i1 100 <| fun w ->
    //        omp.sections 4 <| fun () ->
    //            omp.section <| fun () ->
    //                sub2 (w,1,25)
    //            omp.section <| fun () ->
    //                sub2 (w,26,50)
    //            omp.section <| fun () ->
    //                sub2 (w,51,75)
    //            omp.section <| fun () ->
    //                sub2 (w,76,100)
    //        iter.num w.size1 <| fun i ->
    //            sum <== sum + w[i]
    //        print.c sum
