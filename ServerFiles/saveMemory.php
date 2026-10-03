<?php

$con = mysqli_connect("localhost", "root", "root", "memory");

if (!$con)
{
    die("Connection failed: " . mysqli_connect_error());
}

$name = $_GET["name"];
$colors = $_GET["colors"];
$seconds = $_GET["seconds"];

$query = "INSERT INTO memories (name, colors, seconds)
          VALUES (?, ?, ?)";

$stmt = mysqli_prepare($con, $query);
mysqli_stmt_bind_param($stmt, "sid", $name, $colors, $seconds);

if (mysqli_stmt_execute($stmt))
{
    echo "Memory saved";
}
else
{
    echo "Database error";
}

mysqli_stmt_close($stmt);
mysqli_close($con);

?>