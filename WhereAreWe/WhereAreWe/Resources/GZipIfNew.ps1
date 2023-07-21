param (
   [string]$source
)

$_BufferSize = 1024 * 8

function CompressFile
{param ($in, $out)
	$input = New-Object System.IO.FileStream $in, ([IO.FileMode]::Open), ([IO.FileAccess]::Read), ([IO.FileShare]::Read)
	$output = New-Object System.IO.FileStream $out, ([IO.FileMode]::Create), ([IO.FileAccess]::Write), ([IO.FileShare]::None)
	$gzipStream = New-Object System.IO.Compression.GZipStream($output, ([IO.Compression.CompressionMode]::Compress))
    $buffer = New-Object byte[]($_BufferSize)
	try {
        do
        {
            $read = $input.Read($buffer, 0, $_BufferSize)
            if ($read -gt 0)
            {
                $gzipStream.Write($buffer, 0, $read);
            }
        } while ($read -gt 0)
	}
	finally {
		$gzipStream.Close();
		$output.Close();
		$input.Close();
	}
}

if (!$source) 
{
	Write-Host "Error: No source filename specified"
	exit
}

$sourceFull = (ls -path $source).Fullname
$targetFull = "$sourceFull.gz"
$targDisp = $targetFull -replace "^.*[\\\/]",""

if (!(Test-Path ($sourceFull)))
{
	Write-Host "Could not find file: $source"
	exit
}

Write-Host (Get-Item $sourceFull).LastWriteTime
Write-Host (Get-Item $targetFull).LastWriteTime

if (!(Test-Path ($targetFull)) -or (Get-Item $sourceFull).LastWriteTime -gt (Get-Item $targetFull).LastWriteTime)
{
	# Target file does not exist or is older than the source file
	Write-Host "Compressing: $source"
	CompressFile $sourceFull $targetFull
}
else
{
	Write-Host "Skipping $source ($targDisp is newer)";
}
