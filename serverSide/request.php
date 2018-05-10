<?php
ini_set('display_errors', 1);
ini_set('display_startup_errors', 1);
error_reporting(E_ALL);

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

include_once('db.php');

$req = " ";
if(isset($_GET['key'])){
    $req = $_GET['key'];
}

echo"request = $req </br></br>";

$SQL = "SELECT * FROM fish_data WHERE idx = '$req'";
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

echo"idx = $data_idx </br>";
echo"title = $data_title </br>";
echo"head = $data_head </br>";
echo"body = $data_body </br>";
echo"comment = $data_comment </br>";
echo"views = $data_views </br>";
echo"sugg = $data_sugg </br>";
echo"date = $data_date </br>";


$data = array('idx'=>$data_idx, 'title'=>$data_title, 'head'=>$data_head, 'body'=>$data_body, 'comment'=>$data_comment,
    'views'=>$data_views, 'sugg'=>$data_sugg, 'date'=>$data_date);
$json_sdata = json_encode($data, JSON_PRETTY_PRINT);
echo "</br> JSON = </br>$json_sdata </br>";
?>