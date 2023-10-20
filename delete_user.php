<?php
require_once 'models/UserModel.php';
$userModel = new UserModel();

$user = NULL; //Add new user
$id = NULL;

if (!empty($_GET['id'])) {
    $id = $_GET['id'];
    //$sessionId = $_SESSION['id'];   
    //if ($sessionId == $id) { //Phân quyền người dùng truy cập
    $userModel->deleteUserById($id); //Delete existing user
    //}
}
header('location: list_users.php');
?>