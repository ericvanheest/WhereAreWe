use strict;
use warnings;
use File::stat;

my $source = shift || die "No source filename specified: Error";
my $target = shift || ($source . ".gz");
my $targDisp = $target;
$targDisp =~ s/^.*[\\\/]//;

die "Could not find file \"$source\": Error" if (! -s $source);

if (! -f $target or -M $source < -M $target)
{
	# Target file does not exist or is older than the source file
	print("Compressing $source\n");
	system("gzip -f9k $source");
}
else
{
	print("Skipping $source ($targDisp is newer)\n");
}
