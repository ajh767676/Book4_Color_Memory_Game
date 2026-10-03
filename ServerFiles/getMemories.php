<?php

header("Content-Type: text/plain");

$con = mysqli_connect("localhost", "root", "root", "memory");

if (!$con)
{
    die("Connection failed: " . mysqli_connect_error());
}

$query = "SELECT name, colors, seconds
          FROM memories
          ORDER BY colors DESC, seconds ASC
          LIMIT 5";

$result = mysqli_query($con, $query);

while ($row = mysqli_fetch_assoc($result))
{
    echo $row["name"] . "\t";
    echo $row["colors"] . " colors\t";
    echo $row["seconds"] . " seconds\n";
}

mysqli_close($con);

?>