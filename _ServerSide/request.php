<?php
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

if(isset($_GET['idx'])){ //INDEX로 검색
    $SQL = "SELECT idx FROM fish_data WHERE idx = '$idx'";
    $result_set = mysqli_query($conn, $SQL);
    $result = mysqli_fetch_assoc($result_set);

    $data_idx = $result['idx'];

    if( strcmp(gettype($result['idx']), "NULL") ){
        $strnum = 1;

        $data_title = $result['title'];
        $data_head = $result['head'];
        $data_body = $result['body'];
        $data_comment = $result['comment'];
        $data_views = $result['views'];
        $data_sugg = $result['sugg'];
        $data_date = $result['date'];

        $data = array('idx'=>$data_idx, 'title'=>$data_title, 'head'=>$data_head,
        'body'=>$data_body,'comment'=>$data_comment,'views'=>$data_views, 'sugg'=>$data_sugg, 'date'=>$data_date);

        $JSON = json_encode($data, JSON_PRETTY_PRINT);
    }else{
        $strnum = 0;
    }
}

if(isset($_GET['key'])){ //문자열로 검색
    $strnum = 0;

    $SQL = "SELECT idx FROM fish_data WHERE title LIKE '%$key%'";
    $search_set = mysqli_query($conn, $SQL);
    $strnum = mysqli_num_rows($search_set);
    $search[$strnum] = 0;

    for($i = 0; $i<$strnum; $i++){
        $tmp = mysqli_fetch_array($search_set);
        $search[$i] = $tmp['idx'];
    }

    if($debug){
        echo"<br>";
        print_r($search_set);
        echo"<br><br>";
    }

    $result[$strnum][8] = "0";
    for($i = 0; $i<$strnum; $i++){

        $sidx = $search[$i];
        $SQL = "SELECT * FROM fish_data WHERE idx='$sidx'";
        $row_set = mysqli_query($conn, $SQL);
        $row = mysqli_fetch_assoc($row_set);

        $result[$i][0] = $row['idx'];
        $result[$i][1] = $row['title'];
        $result[$i][2] = $row['head'];
        $result[$i][3] = $row['body'];
        $result[$i][4] = $row['comment'];
        $result[$i][5] = $row['views'];
        $result[$i][6] = $row['sugg'];
        $result[$i][7] = $row['date'];

        if($debug){
            echo"result[$i][0] idx : ".$result[$i][0]."<br>";
            echo"result[$i][1] title : ".$result[$i][1]."<br>";
            echo"result[$i][2] head : ".$result[$i][2]."<br>";
            echo"result[$i][3] body : ".$result[$i][3]."<br>";
            echo"result[$i][4] comment : ".$result[$i][4]."<br>";
            echo"result[$i][5] views : ".$result[$i][5]."<br>";
            echo"result[$i][6] sugg : ".$result[$i][6]."<br>";
            echo"result[$i][7] date : ".$result[$i][7]."<br>";
            echo"===================<br><br>";
        }
    }

    $JSON = json_encode($result, JSON_PRETTY_PRINT);
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

if($strnum <= 0){
    $result = array("NO SEARCH RESULTS");
    $JSON = json_encode($result, JSON_PRETTY_PRINT);
    // die("NO SEARCH RESULTS");
}

print($JSON);

?>