<?php
// Start the session
session_start();
require_once 'models/UserModel.php';
$userModel = new UserModel();

$user = NULL; //Add new user
$_id = NULL;

if (!empty($_GET['id'])) {
    $_id = $_GET['id'];
    $user = $userModel->findUserById($_id); //Update existing user
}

//C1: Sử dụng Anti-Forgery Tokens
// if (!isset($_SESSION['csrf_token'])) {
//     $_SESSION['csrf_token'] = bin2hex(random_bytes(32));
// }

// foreach ($_COOKIE as $name => $value) {
//     setcookie($name, $value, [
//         "expires" => time() + 3600, // Thời gian hết hạn của cookie
//         "path" => "/", // Phạm vi của cookie
//         "domain" => "", // Tên miền của cookie
//         "secure" => true, // Gửi cookie qua kết nối an toàn (HTTPS)
//         "samesite" => "Lax", // Giá trị SameSite (Lax hoặc Strict)
//     ]);
// }

//c3:  Referer Header
// if (!isset($_SERVER['HTTP_REFERER']) || parse_url($_SERVER['HTTP_REFERER'], PHP_URL_HOST) !== 'localhost') {
//     header('HTTP/1.1 403 Forbidden');
//     die('Invalid Referer');
// }


//if (isset($_POST['csrf_token']) && $_POST['csrf_token'] === $_SESSION['csrf_token']) { //c1: sử dụng Anti-Forgery Tokens
if (!empty($_POST['submit'])) {

    if (!empty($_id)) {
        $userModel->updateUser($_POST);
    } else {
        $userModel->insertUser($_POST);
    }
    header('location: list_users.php');
}
// } else {
//     header('location: messege.php');
// }


?>
<!DOCTYPE html>
<html>

<head>
    <title>User form</title>
    <?php include 'views/meta.php' ?>
</head>

<body>
    <?php include 'views/header.php' ?>
    <div class="container">

        <?php if ($user || !isset($_id)) { ?>
        <div class="alert alert-warning" role="alert">
            User form
        </div>
        <form method="POST">
            <!-- <input type="hidden" name="csrf_token" value="<?php //echo $_SESSION['csrf_token']; ?>"> -->
            <input type="hidden" name="id" value="<?php echo $_id ?>">
            <div class="form-group">
                <label for="name">Name</label>
                <input class="form-control" name="name" placeholder="Name" value='<?php if (!empty($user[0]['name']))
                        echo $user[0]['name'] ?>'>
            </div>
            <div class="form-group">
                <label for="password">Password</label>
                <input type="password" name="password" class="form-control" placeholder="Password">
            </div>

            <button type="submit" name="submit" value="submit" class="btn btn-primary">Submit</button>
        </form>
        <?php } else { ?>
        <div class="alert alert-success" role="alert">
            User not found!
        </div>
        <?php } ?>
    </div>
</body>

</html>