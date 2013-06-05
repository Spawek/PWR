function [ ] = RBF_predictor( real_data )

%% constants block
BACKWARD_DATA_USAGE = 4; %0 is no backward data usage (watcha see now)
ROAD_TO_PREDICT_ON = 5;
HOW_FAR_TO_PREDICT = 3;
KOHONEN_ITERATIONS_NO = 1000;

%% prepare training data
training_data = prepare_reduced_training_data();
training_probes = generate_probes(training_data, BACKWARD_DATA_USAGE);
training_probes_no = size(training_probes, 2);
probe_size = size(training_probes, 1);

%% geting probes out of real data
real_data = fix_input_data(real_data);
real_probes = generate_probes(real_data, BACKWARD_DATA_USAGE);

%% generating normalized random weights matrix
weights = rand(training_probes_no, probe_size);
weights = weights / norm(weights);

%% network learing
for n = 1:KOHONEN_ITERATIONS_NO
	for m = 1:training_probes_no
		best_weight = 1;
		for probes = 1:training_probes_no
			
		end
		
	end
end