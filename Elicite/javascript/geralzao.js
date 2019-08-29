// JScript File
function UpdatePainel()
{
    if (document.getElementById('DropDownList2').selectedIndex == 0)
    {
        document.getElementById('admin').display == 'block';
        document.getElementById('oper').display == 'none';
    }
    else if (document.getElementById('DropDownList2').selectedIndex == 2)
    {
        document.getElementById('admin').display == 'block';
        document.getElementById('oper').display == 'none';
    }
}
