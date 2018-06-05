<?php
ini_set('display_errors', 1);
ini_set('display_startup_errors', 1);
error_reporting(E_ALL);

include_once('db.php');
include_once('backside.php');

$publicip = get_client_ip();
$curtime = (new DateTime())->format("Y-m-d H:i:s");

echo "$gen->$publicip";

$key = " ";
if(isset($_GET['key'])){
    $key = $_GET['key'];
}

$result = get_db($key);

$data = array('idx'=>$data_idx, 'title'=>$data_title, 'head'=>$data_head, 'body'=>$data_body, 'comment'=>$data_comment,
    'views'=>$data_views, 'sugg'=>$data_sugg, 'date'=>$data_date);
$json_sdata = json_encode($data, JSON_PRETTY_PRINT);
echo "</br> JSON = </br>$json_sdata </br>";




//$result_set = mysqli_query($conn, "SELECT * FROM dimicon_rank WHERE user_idx='$user_idx'");
//$result = mysqli_fetch_assoc($result_set);
//$rank = $result['rank'];
//$score = $result['score'];
//$_SESSION['score'] = $score;



?>