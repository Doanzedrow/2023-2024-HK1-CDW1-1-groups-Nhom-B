<?php
session_start();
require_once 'models/UserModel.php';
$userModel = new UserModel();

$user = NULL; //Add new user
$id = NULL;

if (!empty($_GET['id'])) {
    $id = $_GET['id'];
    $sessionId = $_SESSION['id'];
    //$resId = base64_decode($id); // Giải mã hóa id
    // $hmac = $_SESSION['hmac']; // c2: lấy hmac từ session
    // $key = 'khonghack'; // c2: Khóa bí mật
    // $server_hmac = hash_hmac('sha256', $id, $key); // c2: Tạo lại mã băm HMAC tại máy chủ
    //if ($sessionId == $id) { //Phân quyền người dùng truy cập
    $user = $userModel->findUserById($id); //Update existing user
    //}
}


if (!empty($_POST['submit'])) {

    if (!empty($id)) {
        $userModel->updateUser($_POST);
    } else {
        $userModel->insertUser($_POST);
    }
    header('location: list_users.php');
}

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
        <?php //if ($hmac === $server_hmac){?>
        <?php if ($user || empty($id)) { ?>

        <div class="alert alert-warning" role="alert">
            User profile
        </div>
        <form method="POST">
            <input type="hidden" name="id" value="<?php echo $id ?>">
            <div class="form-group">
                <label for="name">Name</label>
                <span>
                    <?php if (!empty($user[0]['name']))
                            echo $user[0]['name'] ?>
                </span>
            </div>
            <div class="form-group">
                <label for="password">Fullname</label>
                <span>
                    <?php if (!empty($user[0]['name']))
                            echo $user[0]['fullname'] ?>
                </span>
            </div>
            <div class="form-group">
                <label for="password">Email</label>
                <span>
                    <?php if (!empty($user[0]['name']))
                            echo $user[0]['email'] ?>
                </span>
            </div>
        </form>
        <?php } else { ?>
        <div class="alert alert-success" role="alert">
            User not found!
        </div>
        <?php } ?>
        <?php //} else {
        //header('location: error.php');
        //}
        ?>
    </div>
</body>

</html>