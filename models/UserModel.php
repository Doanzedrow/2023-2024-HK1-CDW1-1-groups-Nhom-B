<?php

require_once 'BaseModel.php';

class UserModel extends BaseModel
{

    public function findUserById($id)
    {
        $sql = 'SELECT * FROM users WHERE id = ' . $id;
        $user = $this->select($sql);

        return $user;
    }

    public function findUser($keyword)
    {
        $sql = 'SELECT * FROM users WHERE user_name LIKE %' . $keyword . '%' . ' OR user_email LIKE %' . $keyword . '%';
        $user = $this->select($sql);

        return $user;
    }

    /**
     * Authentication user
     * @param $userName
     * @param $password
     * @return array
     */
    public function auth($userName, $password)
    {
        $md5Password = md5($password);

        //C1: SQL Escape Functions:
        //$userName = mysqli_real_escape_string(parent::$_connection, $userName);


        $sql = 'SELECT * FROM users WHERE name = "' . $userName . '" AND password = "' . $md5Password . '"';
        $user = $this->select($sql);

        //C2: Prepared Statement
        // $sql = parent::$_connection->prepare('SELECT * FROM users WHERE name=? AND password = ?');
        // $sql->bind_param('ss', $userName, $md5Password);
        // $sql->execute();
        // $result = $sql->get_result();
        // // Lấy dữ liệu từ kết quả
        // $user = $result->fetch_assoc();
        return $user;
    }

    /**
     * Delete user by id
     * @param $id
     * @return mixed
     */
    public function deleteUserById($id)
    {
        $sql = 'DELETE FROM users WHERE id = ' . $id;
        return $this->delete($sql);

    }

    /**
     * Update user
     * @param $input
     * @return mixed
     */
    public function updateUser($input)
    {
        $sql = 'UPDATE users SET 
                 name = "' . mysqli_real_escape_string(self::$_connection, $input['name']) . '", 
                 password="' . md5($input['password']) . '"
                WHERE id = ' . $input['id'];

        $user = $this->update($sql);

        return $user;
    }

    /**
     * Insert user
     * @param $input
     * @return mixed
     */
    public function insertUser($input)
    {
        $sql = "INSERT INTO `app_web1`.`users` (`name`, `password`) VALUES (" .
            "'" . $input['name'] . "', '" . md5($input['password']) . "')";

        $user = $this->insert($sql);

        return $user;
    }

    /**
     * Search users
     * @param array $params
     * @return array
     */
    public function getUsers($params = [])
    {
        //%" UNION SELECT * FROM users -- 
        //Keyword
        if (!empty($params['keyword'])) {
            //C1: Fix union
            // $keyword = mysqli_real_escape_string(parent::$_connection, $params['keyword']);
            // $sql = 'SELECT * FROM users WHERE name LIKE "%' . $keyword . '%"';



            $sql = 'SELECT * FROM users WHERE name LIKE "%' . $params['keyword'] . '%"';
            //Keep this line to use Sql Injection
            //Don't change
            //Example keyword: abcef%";TRUNCATE banks;##
            //$users = self::$_connection->multi_query($sql);
            //Get data
            $users = $this->query($sql);

        } else {
            $sql = 'SELECT * FROM users';
            $users = $this->select($sql);
        }

        return $users;
    }
}