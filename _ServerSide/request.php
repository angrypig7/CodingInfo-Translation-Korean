<?php
// 1. key로 검색시 프리뷰를
// 2. idx로 검색시 마크다운 전체를 리턴하도록

ini_set('display_errors', 1);
ini_set('display_startup_errors', 1);
error_reporting(E_ALL);

include_once('db.php');

function get_client_ip() {
    $ipaddress = '';
    if (getenv('HTTP_CLIENT_IP'))
        $ipaddress = getenv('HTTP_CLIENT_IP');
    else if(getenv('HTTP_X_FORWARDED_FOR'))
        $ipaddress = getenv('HTTP_X_FORWARDED_FOR');
    else if(getenv('HTTP_X_FORWARDED'))
        $ipaddress = getenv('HTTP_X_FORWARDED');
    else if(getenv('HTTP_FORWARDED_FOR'))
        $ipaddress = getenv('HTTP_FORWARDED_FOR');
    else if(getenv('HTTP_FORWARDED'))
        $ipaddress = getenv('HTTP_FORWARDED');
    else if(getenv('REMOTE_ADDR'))
        $ipaddress = getenv('REMOTE_ADDR');
    else
        $ipaddress = 'UNKNOWN';
    return $ipaddress;
}

$publicip = get_client_ip();
$curtime = (new DateTime())->format("Y-m-d H:i:s");

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

if(isset($_GET['idx'])){ //INDEX로 검색
    if($debug){
        echo"<h2>got idx</h2><br>";
    }
    $SQL = "SELECT * FROM fish_data WHERE idx = '$idx'";
    $result_set = mysqli_query($conn, $SQL);
    $result = mysqli_fetch_assoc($result_set);

    $data_idx = $result['idx'];

    if( strcmp(gettype($result['idx']), "NULL") ){
        $strnum = 1;

        $data_title = $result['title'];
        $data_author = $result['author'];
        $data_url = $result['url'];
        $data_body = $result['body'];
        $data_tag = $result['tag'];
        $data_views = $result['views'];
        $data_sugg = $result['sugg'];
        $data_date = $result['date'];

        $JSON = $data_body;
    }else{
        $strnum = 0;
    }

    if($strnum <= 0){
        global $JSON;
        $JSON = "NO SEARCH RESULTS";
        // die("NO SEARCH RESULTS");
    }
}

if(isset($_GET['key'])){ //문자열로 검색
    if($debug){
        echo"<h2>got key</h2><br>";
    }

    $strnum = 0;

    $SQL = "SELECT idx FROM `fish_data` WHERE `title` LIKE '%$key%'";
    $search_set = mysqli_query($conn, $SQL);
    if($search_set != FALSE){
        $strnum = mysqli_num_rows($search_set);
    }else{
        $strnum = 0;
    }
    $search[$strnum] = 0;

    if($strnum <= 0){
        global $JSON;
        $JSON = "NO SEARCH RESULTS";
        // die("NO SEARCH RESULTS");
    }

    for($i = 0; $i<$strnum; $i++){
        $tmp = mysqli_fetch_array($search_set);
        $search[$i] = $tmp['idx'];
    }

    // if($debug){
    //     echo"<br>";
    //     print_r($search_set);
    //     echo"<br><br>";
    // }

    $JSON_temp = array();
    for($i = 0; $i<$strnum; $i++){

        $sidx = $search[$i];
        $SQL = "SELECT * FROM fish_data WHERE idx='$sidx'";
        $row_set = mysqli_query($conn, $SQL);
        $row = mysqli_fetch_assoc($row_set);

        global $JSON_temp;

        // $result[$i] = $row['idx'];
        
        $JSON_temp_arr = array();
        
        // $JSON_temp_arr[0] = utf8_encode($row['idx']);
        // $JSON_temp_arr[1] = utf8_encode($row['title']);
        // $JSON_temp_arr[2] = utf8_encode($row['author']);
        // $JSON_temp_arr[3] = utf8_encode($row['url']);
        // $JSON_temp_arr[4] = utf8_encode($row['tag']);
        // $JSON_temp_arr[5] = utf8_encode($row['views']);
        // $JSON_temp_arr[6] = utf8_encode($row['sugg']);
        // $JSON_temp_arr[7] = utf8_encode($row['date']);

        $JSON_temp_arr[0] = $row['idx'];
        $JSON_temp_arr[1] = $row['title'];
        $JSON_temp_arr[2] = $row['author'];
        $JSON_temp_arr[3] = $row['url'];
        $JSON_temp_arr[4] = $row['tag'];
        $JSON_temp_arr[4] = str_replace(", ", " ", $JSON_temp_arr[4]);  // commas for tags
        $JSON_temp_arr[4] = str_replace(" ", ", ", $JSON_temp_arr[4]);
        $JSON_temp_arr[5] = $row['views'];
        $JSON_temp_arr[6] = $row['sugg'];
        $JSON_temp_arr[7] = $row['date'];
        
        // $JSON_temp[$i] = json_encode($JSON_temp_arr);
        $JSON_temp[$i] = json_encode($JSON_temp_arr, JSON_FORCE_OBJECT);

        // $data = array('idx'=>$row[0], 'title'=>$row[1], 'head'=>$row[2],
        // 'body'=>$row[3],'comment'=>$row[4],'views'=>$row[5], 'sugg'=>$row[6], 'date'=>$row[7]);
        // $JSON_temp = json_encode($data, JSON_PRETTY_PRINT);

        if($debug){
            echo"JSON_temp_arr[0] idx: ".$JSON_temp_arr[0]."<br>";
            echo"JSON_temp_arr[0] title: ".$JSON_temp_arr[1]."<br>";
            echo"JSON_temp_arr[0] author: ".$JSON_temp_arr[2]."<br>";
            echo"JSON_temp_arr[3] url: ".$JSON_temp_arr[3]."<br>";
            echo"JSON_temp_arr[4] tag: ".$JSON_temp_arr[4]."<br>";
            echo"JSON_temp_arr[5] views: ".$JSON_temp_arr[5]."<br>";
            echo"JSON_temp_arr[6] sugg: ".$JSON_temp_arr[6]."<br>";
            echo"JSON_temp_arr[7] date: ".$JSON_temp_arr[7]."<br>";
            echo"JSON_temp[$i]: ";
            print_r($JSON_temp[$i]);
            echo"<br>===========================================<br>";
        }
    }

    if($strnum>0){
        global $JSON;
        $JSON = json_encode(array($JSON_temp));
    }
}

if($debug){
    global $JSON_temp;
    
    echo"<br>idx = $idx </br>";
    echo"key = $key </br>";
    echo"strnum = $strnum </br>";
    echo"JSON_temp: </br>";
    print_r($JSON_temp);
    echo"<br><br>===========================================<br><br>";
}

if($debug == 0){
    header('Content-Type: application/json; charset=utf-8');
}else{
    header('Content-Type: text/html; charset=utf-8');
}

$JSON = stripslashes($JSON);
$JSON = substr($JSON, 1, -1);

$JSON = str_replace("\"{", "{", $JSON);  //fuck
$JSON = str_replace("}\"", "}", $JSON);
$JSON = str_replace("\"[", "[", $JSON);
$JSON = str_replace("]\"", "]", $JSON);
print($JSON);

?>
