<?php
ini_set('display_errors', 1);
ini_set('display_startup_errors', 1);
error_reporting(E_ALL);

include_once('db.php');
include_once('backside.php');

$publicip = get_client_ip();
$curtime = gettime();

if(isset($_GET['idx'])&&isset($_GET['key'])){
    die("INVALID REQUEST FORMAT");
}

$debug = 0;
if(isset($_GET['debug'])){
    $debug = (int)$_GET['debug'];

    if( !(($debug==0) || ($debug==1)) ){
        die("INVALID REQUEST FORMAT");
    }
}

$idx = -1;
if(isset($_GET['idx'])){
    $idx = (int)$_GET['idx'];

    if($idx <= 0){
        die("INVALID REQUEST FORMAT");
    }
}

$key = "";
if(isset($_GET['key'])){
    $key = addslashes((string)$_GET['key']);

    if(!strcmp($key, "")){
        die("INVALID REQUEST FORMAT");
    }
}

$JSON = "";
$strnum = 0;
$result;

if(isset($_GET['idx'])){
    searchIndex($idx, $conn, $debug);
}

if(isset($_GET['key'])){
    searchKey($key, $conn, $debug);
}

if($debug){
    echo"<br>";
    echo"<br>idx = $idx </br>";
    echo"key = $key </br>";
    echo"strnum = $strnum </br>";
    echo"<br>===========================================<br><br>";
    print_r($result);
}

if(!$debug){
    header('Content-Type: application/json;');
}

print($JSON);

?>