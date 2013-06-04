%its just demgmm1.m w/o disp, comments and stuff like that
BACKWARD_DATA_USAGE = 4;
DECREASING_CONST = 10^5;

%% Generate the data
%randn('state', 0); rand('state', 0);
%gmix = gmm(2, 2, 'spherical');
%ndat1 = 20; ndat2 = 20; ndata = ndat1+ndat2;
%gmix.centres =  [0.3 0.3; 0.7 0.7]; 
%gmix.covars = [0.01 0.01];
%x = gmmsamp(gmix, ndata);

%h = figure;
%hd = plot(x(:, 1), x(:, 2), '.g', 'markersize', 30);
%hold on; axis([0 1 0 1]); axis square; set(gca, 'box', 'on');

%% Let's just use my data ;)
cd .. % bad me
training_data = prepare_training_data();
% TUTAJ MACIERZ JEST TRANSPONOWANA!!!
x = generate_probes(training_data, BACKWARD_DATA_USAGE)';
x = x / DECREASING_CONST;
training_data = prepare_reduced_training_data;
%x.label = 'some label';
cd netlab % bad me again
ndata = size(x,1);

%% Set up mixture model
%ncentres = 2; input_dim = 2;
ncentres = size(x,2); input_dim = size(x,2);
mix = gmm(input_dim, ncentres, 'spherical');

%% Initialise the mixture model
%mix.centres = [0.2 0.8; 0.8, 0.2];
%mix.covars = [0.01 0.01];
mix.centres = rand(input_dim);
mix.covars = ones(1, input_dim)*0.01;
mix.covar_type = 'spherical'; 
cd ..

cd netlab

%% Initial E-step.
post = gmmpost(mix, x);
dcols = [post(:,1), zeros(ndata, 1), post(:,2)];

%% M-step.
options = foptions; 
options(14) = 1; % A single iteration
options(1) = -1; % Switch off all messages, including warning
mix = gmmem(mix, x, options);


%% Loop over EM iterations.
numiters = 4;
for n = 1 : numiters

  post = gmmpost(mix, x);
  dcols = [post(:,1), zeros(ndata, 1), post(:,2)];

  [mix, options] = gmmem(mix, x, options);
  fprintf(1, 'Cycle %4d  Error %11.6f\n', n, options(8)/DECREASING_CONST);

end

x = x * DECREASING_CONST;
