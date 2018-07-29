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

$idx = -1;
if(isset($_GET['idx'])){
    $idx = (int)$_GET['idx'];

    if($idx <= 0){
        die("INVALID REQUEST FORMAT");
    }
    else{
    }
}
$key = "";
if(isset($_GET['key'])){
    $key = (string)$_GET['key'];
}

if(($idx==-1&&strcmp($key, "")==0) || ($idx<>-1&&strcmp($key, "")!=0)){
    die("INVALID REQUEST FORMAT");
}
$strnum = 0;





if($idx != -1){
    $strnum = 1;

    $SQL = "SELECT * FROM fish_data WHERE idx = '$idx'";
    $result_set = mysqli_query($conn, $SQL);
    $result = mysqli_fetch_assoc($result_set);

    $data_idx = $result['idx'];
    $data_title = $result['title'];
    $data_head = $result['head'];
    $data_body = $result['body'];
    $data_comment = $result['comment'];
    $data_views = $result['views'];
    $data_sugg = $result['sugg'];
    $data_date = $result['date'];

    $data = array('idx'=>$data_idx, 'title'=>$data_title, 'head'=>$data_head, 'body'=>$data_body, 'comment'=>$data_comment,'views'=>$data_views, 'sugg'=>$data_sugg, 'date'=>$data_date);

    $str[$strnum] = "";
    $str[0] = json_encode($data, JSON_PRETTY_PRINT);
}

if(strcmp($key, "")!=0){
    $SQL = "SELECT idx FROM fish_data WHERE title LIKE '%$key%'";
    $result_set = mysqli_query($conn, $SQL);
    $result = mysqli_fetch_assoc($result_set);

    $data_idx = $result['idx'];
    $data_title = $result['title'];
    $data_head = $result['head'];
    $data_body = $result['body'];
    $data_comment = $result['comment'];
    $data_views = $result['views'];
    $data_sugg = $result['sugg'];
    $data_date = $result['date'];

    $data = array('idx'=>$data_idx, 'title'=>$data_title, 'head'=>$data_head, 'body'=>$data_body, 'comment'=>$data_comment, 'views'=>$data_views, 'sugg'=>$data_sugg, 'date'=>$data_date);

    $str[0] = json_encode($data, JSON_PRETTY_PRINT);
    $str[1] = json_encode($data, JSON_PRETTY_PRINT);
    $str[2] = json_encode($data, JSON_PRETTY_PRINT);
}

$finstr = "";
// $finstr = 
foreach($str as $j){
    if($j>$strnum){
        $strnum++;
    }
    echo"<br>j:<br>";
}

// idx, key변수로 GET으로 받음
// key오 받았을때 multiple row의 결과의 IDX값을 저장했다가 다시 조회해서 최종결고를 넘겨주도록 해야함


echo"<br>===========================================<br>";
$test1 = $str[1];
echo"123$test1";
echo"<br>===========================================<br>";

// $arrdata = array($str, $str, $str);
// $arrdata = print_r($arrdata, TRUE);
// str_replace("\\", "", $arrdata);

$arrdata = "[" . $str[0] . ", " . $str[0] . ", " . $str[0] . "]";
// $json_data = json_encode($arrdata, JSON_PRETTY_PRINT);

echo"$arrdata</br>";

echo"<br>idx = $idx </br>";
echo"key = $key </br>";
echo"title = $data_title </br>";
echo"head = $data_head </br>";
echo"body = $data_body </br>";
echo"comment = $data_comment </br>";
echo"views = $data_views </br>";
echo"sugg = $data_sugg </br>";
echo"date = $data_date </br></br>";
?>